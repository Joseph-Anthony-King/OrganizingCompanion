namespace OrganizingCompanion.Core.Interfaces.Models
{
    internal interface IDomainEntity
    {
        int Id { get; set; }
        string ToJson();
        IDomainEntity Cast<T>();
    }
}
