using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using EricBach.CQRS.QueryRepository;
using EricBach.CQRS.QueryRepository.Response;
using EricBach.LambdaLogger;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pecuniary.Account.Data.ViewModels;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Pecuniary.Account.Events
{
    public class Function
    {
        private static readonly string ElasticSearchDomain = Environment.GetEnvironmentVariable("ElasticSearchDomain");
        private readonly AccountQueryService _accountQueryService;

        public Function()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _accountQueryService = serviceProvider.GetService<AccountQueryService>();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReadRepository<AccountViewModel>, ElasticSearchRepository<AccountViewModel>>();
            serviceCollection.AddScoped<AccountQueryService>();
        }

        public async Task AccountEventHandlerAsync(SNSEvent message, ILambdaContext context)
        {
            Logger.Log($"Received {message.Records.Count} records");

            foreach (var record in message.Records)
            {
                Logger.Log($"Received message {record.Sns.Message}");

                Logger.Log("Adding Account document");
                var response = await _accountQueryService.AddOrUpdateAsync(record.Sns.Message);

                Logger.Log($"Add Account Result: {response.Result}");
            }

            Logger.Log($"Completed processing {message.Records.Count} records");
        }

        public class AccountQueryService
        {
            private readonly IReadRepository<AccountViewModel> _repository;

            public AccountQueryService(IReadRepository<AccountViewModel> repository)
            {
                _repository = repository;
            }

            public async Task<ElasticSearchResponse> AddOrUpdateAsync(string message)
            {
                Logger.Log("Adding document to ElasticSearch");
                
                // Get the event name from the message
                var eventName = Regex.Matches(message, @"EventName"":[\s]*""([a-zA-Z)]+)").First().Groups.Last().Value;
                Logger.Log($"Event Name: {eventName}");

                // Dynamically convert deserialized event to event type
                dynamic request = JsonConvert.DeserializeObject(message, Type.GetType(eventName));
                Logger.Log($"Event Id: {request.Id}");

                // Use the domain model name (by parsing the first word in the event name) as the index
                var index = Regex.Matches(eventName, @"([A-Z][a-z]+)").Select(m => m.Value).First().ToLower();
                Logger.Log($"Event Index: {index}");

                Logger.Log($"Sending document {request.Id} to ElasticSearch {ElasticSearchDomain}");
                return await _repository.AddAsync(ElasticSearchDomain, index, request.Id.ToString(), message);
            }
        }
    }
}
