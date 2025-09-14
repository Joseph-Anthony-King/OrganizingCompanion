using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;

namespace OrganizingCompanion.Core.Models
{
    internal class Shift : IShift
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
        DateTime IShift.StartDateTime { get => StartDateTime; set => StartDateTime = value; }
        DateTime IShift.EndDateTime { get => EndDateTime; set => EndDateTime = value; }
        int IShift.UserId { get => UserId; set => UserId = value; }
        User? IShift.User { get => User; set => User = value; }
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("startDateTime")] public DateTime StartDateTime { get; set; }
        [Required, JsonPropertyName("endDateTime")] public DateTime EndDateTime { get; set; }
        [Required, JsonPropertyName("userId")] public int UserId { get; set; } = 0;
        [Required, JsonPropertyName("user")] public User? User { get; set; } = null;
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [JsonPropertyName("dateModified"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        public override string ToString() => string.Format(
            base.ToString() + ".Id:{0}.StartDateTime:{1}.EndDateTime:{2}.UserName:{3}",
            Id, StartDateTime, EndDateTime, EndDateTime);
        #endregion
    }
}
