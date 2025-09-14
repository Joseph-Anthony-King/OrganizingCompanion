namespace OrganizingCompanion.Core.Interfaces.Models.DomainEntities
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
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}