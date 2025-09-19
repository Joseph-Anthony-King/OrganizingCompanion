using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;

[assembly: InternalsVisibleTo("OrganizingCompanion.Test")]
namespace OrganizingCompanion.Core.Models
{
    internal class UserShift : IUserShift
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
        int IUserShift.UserId { get => UserId; set => UserId = value; }
        int IUserShift.ShiftId { get => ShiftId; set => ShiftId = value; }
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("userId")] public int UserId { get; set; } = 0;
        [Required, JsonPropertyName("shiftId")] public int ShiftId { get; set; } = 0;
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [Required, JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public UserShift() { }

        [JsonConstructor]
        public UserShift(
            int id,
            int userId,
            int shiftId,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            UserId = userId;
            ShiftId = shiftId;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        public override string ToString() => string.Format(
            base.ToString() + ".Id:{0}.UserId:{1}.ShiftId:{2}",
            Id, UserId, ShiftId);
        #endregion
    }
}
