using System;
using System.Threading.Tasks;
using EricBach.CQRS.QueryRepository.Response;

namespace EricBach.CQRS.QueryRepository
{
    public interface IReadRepository<T> where T : class
    {
        Task<string> GetByIdAsync(Guid id);
        Task<ElasticSearchResponse> AddOrUpdateAsync(string message, string model, string id);
    }
}