using OrganizingCompanion.Core.Interfaces.Models;

namespace OrganizingCompanion.Scheduler.Interfaces.Models.DomainEntities
{
    internal interface IShift : IDomainEntity
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        int UserId { get; set; }
    }
}
