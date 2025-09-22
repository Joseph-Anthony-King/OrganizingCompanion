using OrganizingCompanion.Core.Interfaces.Models;

namespace OrganizingCompanion.Scheduler.Interfaces.Models
{
    internal interface IIcsFile : IDomainEntity
    {
        string FileName { get; set; }
        string? Description { get; set; }
        string ContentType { get; set; }
        byte[] FileContent { get; set; }
        long FileSize { get; set; }
        string? OriginalFileName { get; set; }
        int? CreatedByUserId { get; set; }
    }
}
