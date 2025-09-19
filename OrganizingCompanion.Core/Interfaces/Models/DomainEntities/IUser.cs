namespace OrganizingCompanion.Core.Interfaces.Models.DomainEntities
{
    internal interface IUser : IDomainEntity
    {
        string UserName { get; set; }
        string? FirstName { get; set; }
        string? LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string? Password { get; set; }
        List<IShift> Shifts { get; set; }
        bool IsOrganizer { get; set; }
        void ScrubPassword();
    }
}
