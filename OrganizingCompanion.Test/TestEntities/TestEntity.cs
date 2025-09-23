using System.Text.Json;
using OrganizingCompanion.Core.Interfaces.Models;

namespace OrganizingCompanion.Test.TestEntities
{
    internal class TestEntity : IDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public IDomainEntity Cast<T>() => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this);
    }
}