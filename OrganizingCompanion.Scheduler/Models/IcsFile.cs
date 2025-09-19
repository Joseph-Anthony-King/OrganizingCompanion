using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;

[assembly:InternalsVisibleTo("OrganizingCompanion.Test")]
namespace OrganizingCompanion.Scheduler.Models
{
    internal class IcsFile : IIcsFile
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
        string IIcsFile.FileName { get => FileName; set => FileName = value; }
        string? IIcsFile.Description { get => Description; set => Description = value; }
        string IIcsFile.ContentType { get => ContentType; set => ContentType = value; }
        byte[] IIcsFile.FileContent { get => FileContent; set => FileContent = value; }
        long IIcsFile.FileSize { get => FileSize; set => FileSize = value; }
        string? IIcsFile.OriginalFileName { get => OriginalFileName; set => OriginalFileName = value; }
        int? IIcsFile.CreatedByUserId { get => CreatedByUserId; set => CreatedByUserId = value; }
        DateTime IDomainEntity.DateCreated { get => DateCreated; set => DateCreated = value; }
        DateTime? IDomainEntity.DateModified { get => DateModified; set => DateModified = value; }
        #endregion

        [Required, JsonPropertyName("id")] public int Id { get; set; } = 0;
        [Required, JsonPropertyName("fileName")] public string FileName { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string? Description { get; set; } = null;
        [Required, JsonPropertyName("contentType")] public string ContentType { get; set; } = "text/calendar";
        [Required, JsonPropertyName("fileContent")] public byte[] FileContent { get; set; } = [];
        [Required, JsonPropertyName("fileSize")] public long FileSize { get; set; } = 0;
        [JsonPropertyName("originalFileName")] public string? OriginalFileName { get; set; } = null;
        [JsonPropertyName("createdByUserId")] public int? CreatedByUserId { get; set; } = null;
        [Required, JsonPropertyName("dateCreated")] public DateTime DateCreated { get; set; } = default;
        [Required, JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public IcsFile() { }

        [JsonConstructor]
        public IcsFile(
            int id,
            string fileName,
            string? description,
            string contentType,
            byte[] fileContent,
            long fileSize,
            string? originalFileName,
            int? createdByUserId,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            FileName = fileName;
            Description = description;
            ContentType = contentType;
            FileContent = fileContent;
            FileSize = fileSize;
            OriginalFileName = originalFileName;
            CreatedByUserId = createdByUserId;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        public override string ToString() => string.Format(base.ToString() + ".Id:{0}.FileName:{1}", Id, FileName);
        #endregion
    }
}
