using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Scheduler.Interfaces.Models;

[assembly: InternalsVisibleTo("OrganizingCompanion.Test")]
namespace OrganizingCompanion.Scheduler.Models
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
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("startDateTime")] public DateTime StartDateTime { get; set; }
        [Required, JsonPropertyName("endDateTime")] public DateTime EndDateTime { get; set; }
        [Required, JsonPropertyName("userId")] public int UserId { get; set; } = 0;
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [Required, JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public Shift() { }

        [JsonConstructor]
        public Shift(
            int id,
            DateTime startDateTime,
            DateTime endDateTime,
            int userId,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            UserId = userId;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        public override string ToString() => string.Format(
            base.ToString() + ".Id:{0}.StartDateTime:{1}.EndDateTime:{2}",
            Id, StartDateTime, EndDateTime);
        #endregion
    }
}
