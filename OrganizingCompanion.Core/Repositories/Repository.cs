using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Repositories;

[assembly: InternalsVisibleTo("OrganizingCompanion.Test")]
namespace OrganizingCompanion.Core.Repositories
{
    internal class Repository<TEntity>(DbContext context, ILogger<Repository<TEntity>> logger) : IRepository<TEntity> where TEntity : class, IDomainEntity
    {
        #region Fields
        private readonly DbContext _context = context;
        private readonly ILogger<Repository<TEntity>> _logger = logger;
        #endregion

        #region Methods
        public async Task<TEntity> GetAsync(int id)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(0, id, nameof(id));

                var dbSet = _context.Set<TEntity>();

                var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entity of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                var dbSet = _context.Set<TEntity>();

                var result = await dbSet.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all entities of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, nameof(entity));

                ArgumentOutOfRangeException.ThrowIfNotEqual(0, entity.Id, nameof(entity.Id));

                var dbSet = _context.Set<TEntity>();

                await dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, nameof(entity));

                ArgumentOutOfRangeException.ThrowIfNotEqual(0, entity.Id, nameof(entity.Id));

                var dbSet = _context.Set<TEntity>();

                dbSet.Remove(entity);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> DeleteRangeAsync(List<TEntity> entities)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entities, nameof(entities));

                ArgumentOutOfRangeException.ThrowIfEqual(0, entities.Count, nameof(entities));

                var dbSet = _context.Set<TEntity>();

                dbSet.RemoveRange(entities);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entities of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);

                var dbSet = _context.Set<TEntity>();

                if (await dbSet.AnyAsync(x => x.Id != entity.Id))
                {
                    throw new Exception(string.Format("Error finding entity of type {0}", typeof(TEntity).Name));
                }

                dbSet.Update(entity);

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entities, nameof(entities));

                ArgumentOutOfRangeException.ThrowIfEqual(0, entities.Count, nameof(entities));

                var dbSet = _context.Set<TEntity>();

                dbSet.RemoveRange(entities);

                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entities of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> HasEntityAsync(int id)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(0, id, nameof(id));

                var dbSet = _context.Set<TEntity>();

                var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

                return result != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming existence of entity of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }
        #endregion
    }
}
