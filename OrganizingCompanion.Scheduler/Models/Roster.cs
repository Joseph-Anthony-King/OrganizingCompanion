using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;
using OrganizingCompanion.Scheduler.Interfaces.Models;

namespace OrganizingCompanion.Scheduler.Models
{
    internal class Roster : IRoster
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
        List<IUser> IRoster.Users { get => Users; set => Users = value; }
        DateTime IRoster.StartDateTime { get => StartDateTime; set => StartDateTime = value; }
        DateTime IRoster.EndDateTime { get => EndDateTime; set => EndDateTime = value; }
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("users")] public List<IUser> Users { get; set; } = [];
        [Required, JsonPropertyName("completed")] public bool Completed { get; set; } = false;
        [Required, JsonPropertyName("startDateTime")] public DateTime StartDateTime { get; set; } = default;
        [Required, JsonPropertyName("endDateTime")] public DateTime EndDateTime { get; set; } = default;
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [Required, JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public Roster() { }

        [JsonConstructor]
        public Roster(
            int id,
            List<IUser> users,
            bool completed,
            DateTime startDateTime,
            DateTime endDateTime,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            Users = users;
            Completed = completed;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
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
