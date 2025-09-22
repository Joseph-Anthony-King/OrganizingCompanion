using OrganizingCompanion.Core.Interfaces.Models;

namespace OrganizingCompanion.Core.Interfaces.Repositories
{
    internal interface IRepository<T> where T : IDomainEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(List<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteRangeAsync(List<T> entities);
        Task<bool> HasEntityAsync(int id);
    }
}