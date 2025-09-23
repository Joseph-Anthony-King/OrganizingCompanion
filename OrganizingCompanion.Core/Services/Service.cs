using Microsoft.Extensions.Logging;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Repositories;
using OrganizingCompanion.Core.Interfaces.Services;

namespace OrganizingCompanion.Core.Services
{
    internal class Service<T>(IRepository<T> repository, ILogger<Service<T>> logger) : IService<T> where T : IDomainEntity
    {
        private readonly IRepository<T> _repository = repository;
        private readonly ILogger<Service<T>> _logger = logger;

        public async Task<T> GetAsync(int id)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0, nameof(id));

                return await _repository.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entity with ID {EntityId}", id);
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all entities");
                throw;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, nameof(entity));

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(entity.Id, 0, nameof(entity.Id));

                return await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding entity with ID {EntityId}", entity?.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, nameof(entity));

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(entity.Id, 0, nameof(entity.Id));

                return await _repository.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity with ID {EntityId}", entity?.Id);
                throw;
            }
        }

        public async Task<bool> DeleteRangeAsync(List<T> entities)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entities, nameof(entities));

                ArgumentOutOfRangeException.ThrowIfEqual(entities.Count, 0, nameof(entities));

                return await _repository.DeleteRangeAsync(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting range of entities");
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, nameof(entity));

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(entity.Id, 0, nameof(entity.Id));

                return await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity with ID {EntityId}", entity?.Id);
                throw;
            }
        }

        public async Task<List<T>> UpdateRangeAsync(List<T> entities)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entities, nameof(entities));

                ArgumentOutOfRangeException.ThrowIfEqual(entities.Count, 0, nameof(entities));

                var result = await _repository.UpdateRangeAsync(entities);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating range of entities");
                throw;
            }
        }

        public Task<bool> HasEntityAsync(int id)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0, nameof(id));

                return _repository.HasEntityAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking existence of entity with ID {EntityId}", id);
                throw;
            }
        }
    }
}