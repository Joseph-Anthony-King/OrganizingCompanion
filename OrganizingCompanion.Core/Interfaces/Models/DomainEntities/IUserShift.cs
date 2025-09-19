namespace OrganizingCompanion.Core.Interfaces.Models.DomainEntities
{
    internal interface IUserShift : IDomainEntity
    {
        int UserId { get; set; }
        int ShiftId { get; set; }
    }
}
