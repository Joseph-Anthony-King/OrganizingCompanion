using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;

namespace OrganizingCompanion.Core.Models
{
    internal class DomainEntity : IDomainEntity
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        #endregion

        #region Properties
        #region Explicit interface implementation
        int IDomainEntity.Id { get => Id; set => Id = value; }
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [Required, JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public DomainEntity() {}

        [JsonConstructor]
        public DomainEntity(
            int id,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        public override string ToString() => string.Format(
            base.ToString() + ".Id:{0}",
            Id);
        #endregion
    }
}