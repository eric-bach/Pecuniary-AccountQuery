using System;
using System.Threading;
using System.Threading.Tasks;
using EricBach.CQRS.QueryRepository;
using EricBach.LambdaLogger;
using MediatR;
using Newtonsoft.Json;
using Pecuniary.Account.Data.Models;
using Pecuniary.Account.Query.Queries;

namespace Pecuniary.Account.Query.QueryHandlers
{
    public class AccountQueryHandlers : IRequestHandler<GetAccountQuery, AccountReadModel>
    {
        private readonly IReadRepository<AccountReadModel> _repository;

        public AccountQueryHandlers(IReadRepository<AccountReadModel> repository)
        {
            _repository = repository ?? throw new InvalidOperationException("Repository is not initialized.");
        }

        public async Task<AccountReadModel> Handle(GetAccountQuery query, CancellationToken cancellationToken)
        {
            Logger.Log($"{nameof(GetAccountQuery)} handler invoked");

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var response = await _repository.GetByIdAsync(query.Id);

            // Deserialize back to read model
            return JsonConvert.DeserializeObject<AccountReadModel>(response);
        }
    }
}
