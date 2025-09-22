using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using OrganizingCompanion.Core.Interfaces.Repositories;
using OrganizingCompanion.Scheduler.Interfaces.Models;
using OrganizingCompanion.Scheduler.Interfaces.Services;
using OrganizingCompanion.Scheduler.Models;

[assembly: InternalsVisibleTo("OrganizingCompanion.Test")]
namespace OrganizingCompanion.Scheduler.Services
{
    internal class IcsFileService(IRepository<IcsFile> repository, ILogger<IcsFileService> logger) : IIcsFileService
    {
        private readonly IRepository<IcsFile> _repository = repository;
        private readonly ILogger<IcsFileService> _logger = logger;

        public async Task<IIcsFile> GetAsync(int id)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0, nameof(id));

                return await _repository.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ICS file with ID {IcsFileId}", id);
                throw;
            }
        }

        public async Task<List<IIcsFile>> GetAllAsync()
        {
            try
            {
                return (await _repository.GetAllAsync()).ConvertAll(x => (IIcsFile)x);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all ICS files");
                throw;
            }
        }

        public async Task<IIcsFile> AddAsync(IIcsFile icsFile)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(icsFile, nameof(icsFile));

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(icsFile.Id, 0, nameof(icsFile.Id));
                
                return await _repository.AddAsync((IcsFile)icsFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding ICS file with ID {IcsFileId}", icsFile?.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(IIcsFile icsFile)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(icsFile, nameof(icsFile));

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(icsFile.Id, 0, nameof(icsFile.Id));

                return await _repository.DeleteAsync((IcsFile)icsFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting ICS file with ID {IcsFileId}", icsFile?.Id);
                throw;
            }
        }

        public async Task<bool> DeleteRangeAsync(List<IIcsFile> icsFiles)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(icsFiles, nameof(icsFiles));

                ArgumentOutOfRangeException.ThrowIfEqual(icsFiles.Count, 0, nameof(icsFiles));

                return await _repository.DeleteRangeAsync(icsFiles.ConvertAll(x => (IcsFile)x));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting range of ICS files");
                throw;
            }
        }

        public async Task<IIcsFile> UpdateAsync(IIcsFile icsFile)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(icsFile, nameof(icsFile));

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(icsFile.Id, 0, nameof(icsFile.Id));

                return await _repository.UpdateAsync((IcsFile)icsFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ICS file with ID {IcsFileId}", icsFile?.Id);
                throw;
            }
        }

        public async Task<List<IIcsFile>> UpdateRangeAsync(List<IIcsFile> icsFiles)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(icsFiles, nameof(icsFiles));

                ArgumentOutOfRangeException.ThrowIfEqual(icsFiles.Count, 0, nameof(icsFiles));

                var result = await _repository.UpdateRangeAsync(icsFiles.ConvertAll(x => (IcsFile)x));
                return result.ConvertAll(x => (IIcsFile)x);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating range of ICS files");
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
                _logger.LogError(ex, "Error checking existence of ICS file with ID {IcsFileId}", id);
                throw;
            }
        }
    }
}
