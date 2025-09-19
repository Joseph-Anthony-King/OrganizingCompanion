using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OrganizingCompanion.Scheduler")]
[assembly: InternalsVisibleTo("OrganizingCompanion.Test")]

namespace OrganizingCompanion.Core.Interfaces.Models
{
    internal interface IDomainEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        string ToJson();
        IDomainEntity Cast<T>();
    }
}
