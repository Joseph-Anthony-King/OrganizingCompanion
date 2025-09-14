using OrganizingCompanion.Core.Models;

namespace OrganizingCompanion.Core.Interfaces.Models.DomainEntities
{
    internal interface IShift : IDomainEntity
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        int UserId { get; set; }
        User? User { get; set; }
    }
}
