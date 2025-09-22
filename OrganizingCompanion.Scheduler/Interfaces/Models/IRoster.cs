using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;

namespace OrganizingCompanion.Scheduler.Interfaces.Models
{
    internal interface IRoster : IDomainEntity
    {
        List<IUser> Users { get; set; }
        bool Completed { get; set; }
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
    }
}
