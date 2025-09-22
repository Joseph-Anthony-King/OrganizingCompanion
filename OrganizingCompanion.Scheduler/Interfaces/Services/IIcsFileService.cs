using OrganizingCompanion.Scheduler.Interfaces.Models;

namespace OrganizingCompanion.Scheduler.Interfaces.Services
{
    internal interface IIcsFileService
    {
        Task<IIcsFile> AddAsync(IIcsFile icsFile);
        Task<IIcsFile> GetAsync(int id);
        Task<List<IIcsFile>> GetAllAsync();
        Task<IIcsFile> UpdateAsync(IIcsFile icsFile);
        Task<List<IIcsFile>> UpdateRangeAsync(List<IIcsFile> icsFiles);
        Task<bool> DeleteAsync(IIcsFile icsFile);
        Task<bool> DeleteRangeAsync(List<IIcsFile> icsFiles);
        Task<bool> HasEntityAsync(int id);
    }
}
