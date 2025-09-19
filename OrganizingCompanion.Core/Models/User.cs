using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;

[assembly:InternalsVisibleTo("OrganizingCompanion.Test")]
namespace OrganizingCompanion.Core.Models
{
    internal class User : IUser
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
        string IUser.UserName { get => UserName; set => UserName = value; }
        string? IUser.FirstName { get => FirstName; set => FirstName = value; }
        string? IUser.LastName { get => LastName; set => LastName = value; }
        string IUser.Email { get => Email; set => Email = value; }
        string IUser.Phone { get => Phone; set => Phone = value; }
        string? IUser.Password { get => Password ?? string.Empty; set => Password = value; }
        bool IUser.IsOrganizer { get => IsOrganizer; set => IsOrganizer = value; }
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("username")] public string UserName { get; set; } = string.Empty;
        [JsonPropertyName("firstName")] public string? FirstName { get; set; } = null;
        [JsonPropertyName("lastName")] public string? LastName { get; set; } = null;
        [Required, JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
        [Required, JsonPropertyName("phone")] public string Phone { get; set; } = string.Empty;
        [JsonPropertyName("password"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public string? Password { get; set; } = null;
        [Required, JsonPropertyName("isOrganizer")] public bool IsOrganizer { get; set; }
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [Required, JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public User() { }

        [JsonConstructor]
        public User(
            int id,
            string username,
            string? firstName,
            string? lastName,
            string email,
            string phone,
            string? password,
            bool isOrganizer,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            IsOrganizer = isOrganizer;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public void ScrubPassword() => Password = null;
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        public override string ToString() => string.Format(base.ToString() + ".Id:{0}.UserName:{1}", Id, UserName);
        #endregion
    }
}
