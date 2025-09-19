namespace OrganizingCompanion.Core.Interfaces.Models.DomainEntities
{
    internal interface IRoster : IDomainEntity
    {
        List<IUser> Users { get; set; }
        List<IShift> Shifts { get; set; }
        bool Completed { get; set; }
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
    }
}
