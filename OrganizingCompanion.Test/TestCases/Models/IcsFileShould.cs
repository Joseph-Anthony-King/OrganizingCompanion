using System.Text;
using System.Text.Json;
using NUnit.Framework;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;
using OrganizingCompanion.Core.Models;

namespace OrganizingCompanion.Test.TestCases.Models
{
    public class IcsFileShould
    {
        private IIcsFile? sut;

        [SetUp]
        public void Setup()
        {
            sut = new IcsFile
            {
                Id = 1,
                FileName = "test.ics",
                Description = "Test ICS file",
                ContentType = "text/calendar",
                FileContent = Encoding.UTF8.GetBytes("BEGIN:VCALENDAR...END:VCALENDAR"),
                FileSize = 1234,
                OriginalFileName = "original_test.ics",
                CreatedByUserId = 42,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
        }

        [Test, Category("Models")]
        public void BeADomainEntity()
        {
            // Arrange and Act

            // Assert
            Assert.That(sut, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("Models")]
        public void HaveAnId()
        {
            // Arrange and Act

            // Assert
            Assert.That(sut?.Id, Is.EqualTo(1));
        }

        [Test, Category("Models")]
        public void HaveTheExpectedProperties()
        {
            // Arrange and Act

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut?.Id, Is.TypeOf<int>());
                Assert.That(sut?.FileName, Is.TypeOf<string>());
                Assert.That(sut?.FileName, Is.EqualTo("test.ics"));
                Assert.That(sut?.Description, Is.TypeOf<string>());
                Assert.That(sut?.Description, Is.EqualTo("Test ICS file"));
                Assert.That(sut?.ContentType, Is.TypeOf<string>());
                Assert.That(sut?.ContentType, Is.EqualTo("text/calendar"));
                Assert.That(sut?.FileContent, Is.TypeOf<byte[]>());
                Assert.That(sut?.FileContent, Is.EqualTo(Encoding.UTF8.GetBytes("BEGIN:VCALENDAR...END:VCALENDAR")));
                Assert.That(sut?.FileSize, Is.TypeOf<long>());
                Assert.That(sut?.FileSize, Is.EqualTo(1234));
                Assert.That(sut?.OriginalFileName, Is.TypeOf<string>());
                Assert.That(sut?.OriginalFileName, Is.EqualTo("original_test.ics"));
                Assert.That(sut?.CreatedByUserId, Is.TypeOf<int>());
                Assert.That(sut?.CreatedByUserId, Is.EqualTo(42));
                Assert.That(sut?.DateCreated, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(sut?.DateModified, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void HaveAParameterlessConstructor()
        {
            // Arrange and Act
            var icsFile = new IcsFile();
            // Assert
            Assert.That(icsFile, Is.Not.Null);
            Assert.That(icsFile.Id, Is.EqualTo(0));
            Assert.That(icsFile.FileName, Is.EqualTo(string.Empty));
            Assert.That(icsFile.Description, Is.Null);
            Assert.That(icsFile.ContentType, Is.EqualTo("text/calendar"));
            Assert.That(icsFile.FileContent, Is.EqualTo(Array.Empty<byte>()));
            Assert.That(icsFile.FileSize, Is.EqualTo(0));
            Assert.That(icsFile.OriginalFileName, Is.Null);
            Assert.That(icsFile.CreatedByUserId, Is.Null);
            Assert.That(icsFile.DateCreated, Is.EqualTo(default(DateTime)));
            Assert.That(icsFile.DateModified, Is.EqualTo(default(DateTime)));
        }

        [Test, Category("Models")]
        public void HaveAJsonConstructor()
        {
            // Arrange and Act
            var icsFile = new IcsFile(
                1,
                "test.ics",
                "Test ICS file",
                "text/calendar",
                Encoding.UTF8.GetBytes("BEGIN:VCALENDAR...END:VCALENDAR"),
                1234,
                "original_test.ics",
                42,
                DateTime.UtcNow,
                DateTime.UtcNow);

            // Assert
            Assert.That(icsFile, Is.Not.Null);
            Assert.That(icsFile.Id, Is.EqualTo(1));
            Assert.That(icsFile.FileName, Is.EqualTo("test.ics"));
            Assert.That(icsFile.Description, Is.EqualTo("Test ICS file"));
            Assert.That(icsFile.ContentType, Is.EqualTo("text/calendar"));
            Assert.That(icsFile.FileContent, Is.EqualTo(Encoding.UTF8.GetBytes("BEGIN:VCALENDAR...END:VCALENDAR")));
            Assert.That(icsFile.FileSize, Is.EqualTo(1234));
            Assert.That(icsFile.OriginalFileName, Is.EqualTo("original_test.ics"));
            Assert.That(icsFile.CreatedByUserId, Is.EqualTo(42));
            Assert.That(icsFile.DateCreated, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(icsFile.DateModified, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test, Category("Models")]
        public void SerializeToJson()
        {
            // Arrange
            var expectedJsonStart = "{\"id\":1,\"fileName\":\"test.ics\",\"description\":\"Test ICS file\",\"contentType\":\"text/calendar\",\"fileContent\":\"";
            var expectedJsonEnd = "\",\"fileSize\":1234,\"originalFileName\":\"original_test.ics\",\"createdByUserId\":42";
            // Act
            var json = sut?.ToJson();
            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.StartWith(expectedJsonStart));
            Assert.That(json, Does.Contain(expectedJsonEnd));
        }

        [Test, Category("Models")]
        public void OverrideToString()
        {
            // Arrange
            var expectedString = "OrganizingCompanion.Core.Models.IcsFile.Id:1.FileName:test.ics";
            // Act
            var toString = sut?.ToString();
            // Assert
            Assert.That(toString, Is.TypeOf<string>());
            Assert.That(toString, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void ThrowNotImplementedExceptionWhenCastIsCalled()
        {
            // Arrange
            var domainEntity = sut as IDomainEntity;
            
            // Act & Assert
            Assert.That(() => domainEntity?.Cast<IcsFile>(), Throws.TypeOf<NotImplementedException>());
        }

        [Test, Category("Models")]
        public void AllowSettingPropertiesThroughExplicitInterfaceImplementation()
        {
            // Arrange
            var icsFile = new IcsFile();
            var domainEntity = icsFile as IDomainEntity;
            var iIcsFile = icsFile as IIcsFile;
            
            // Act
            domainEntity!.Id = 999;
            iIcsFile!.FileName = "interface_test.ics";
            iIcsFile.Description = "Interface description";
            iIcsFile.ContentType = "application/calendar";
            iIcsFile.FileContent = Encoding.UTF8.GetBytes("test content");
            iIcsFile.FileSize = 5678;
            iIcsFile.OriginalFileName = "original_interface.ics";
            iIcsFile.CreatedByUserId = 123;
            var testDate = DateTime.UtcNow;
            iIcsFile.DateCreated = testDate;
            iIcsFile.DateModified = testDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(icsFile.Id, Is.EqualTo(999));
                Assert.That(icsFile.FileName, Is.EqualTo("interface_test.ics"));
                Assert.That(icsFile.Description, Is.EqualTo("Interface description"));
                Assert.That(icsFile.ContentType, Is.EqualTo("application/calendar"));
                Assert.That(icsFile.FileContent, Is.EqualTo(Encoding.UTF8.GetBytes("test content")));
                Assert.That(icsFile.FileSize, Is.EqualTo(5678));
                Assert.That(icsFile.OriginalFileName, Is.EqualTo("original_interface.ics"));
                Assert.That(icsFile.CreatedByUserId, Is.EqualTo(123));
                Assert.That(icsFile.DateCreated, Is.EqualTo(testDate));
                Assert.That(icsFile.DateModified, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void DeserializeFromJson()
        {
            // Arrange
            var json = """
            {
                "id": 5,
                "fileName": "deserialized.ics",
                "description": "Deserialized file",
                "contentType": "text/calendar",
                "fileContent": "VEVTVCBDT05URU5U",
                "fileSize": 2048,
                "originalFileName": "original_deserialized.ics",
                "createdByUserId": 99,
                "dateCreated": "2024-01-01T12:00:00Z",
                "dateModified": "2024-01-02T12:00:00Z"
            }
            """;

            // Act
            var deserializedFile = JsonSerializer.Deserialize<IcsFile>(json);

            // Assert
            Assert.That(deserializedFile, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserializedFile.Id, Is.EqualTo(5));
                Assert.That(deserializedFile.FileName, Is.EqualTo("deserialized.ics"));
                Assert.That(deserializedFile.Description, Is.EqualTo("Deserialized file"));
                Assert.That(deserializedFile.ContentType, Is.EqualTo("text/calendar"));
                Assert.That(deserializedFile.FileContent, Is.EqualTo(Convert.FromBase64String("VEVTVCBDT05URU5U")));
                Assert.That(deserializedFile.FileSize, Is.EqualTo(2048));
                Assert.That(deserializedFile.OriginalFileName, Is.EqualTo("original_deserialized.ics"));
                Assert.That(deserializedFile.CreatedByUserId, Is.EqualTo(99));
                Assert.That(deserializedFile.DateCreated, Is.EqualTo(new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc)));
                Assert.That(deserializedFile.DateModified, Is.EqualTo(new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Utc)));
            });
        }

        [Test, Category("Models")]
        public void HandleNullValuesInJsonConstructor()
        {
            // Arrange and Act
            var icsFile = new IcsFile(
                0,
                string.Empty,
                null,
                "text/calendar",
                Array.Empty<byte>(),
                0,
                null,
                null,
                default,
                default);

            // Assert
            Assert.That(icsFile, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(icsFile.Id, Is.EqualTo(0));
                Assert.That(icsFile.FileName, Is.EqualTo(string.Empty));
                Assert.That(icsFile.Description, Is.Null);
                Assert.That(icsFile.ContentType, Is.EqualTo("text/calendar"));
                Assert.That(icsFile.FileContent, Is.EqualTo(Array.Empty<byte>()));
                Assert.That(icsFile.FileSize, Is.EqualTo(0));
                Assert.That(icsFile.OriginalFileName, Is.Null);
                Assert.That(icsFile.CreatedByUserId, Is.Null);
                Assert.That(icsFile.DateCreated, Is.EqualTo(default(DateTime)));
                Assert.That(icsFile.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void SerializeAndDeserializeRoundTrip()
        {
            // Arrange
            var original = new IcsFile(
                42,
                "roundtrip.ics",
                "Round trip test",
                "text/calendar",
                Encoding.UTF8.GetBytes("BEGIN:VCALENDAR\nEND:VCALENDAR"),
                28,
                "original_roundtrip.ics",
                100,
                DateTime.UtcNow,
                DateTime.UtcNow);

            // Act
            var json = original.ToJson();
            var deserialized = JsonSerializer.Deserialize<IcsFile>(json);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserialized.Id, Is.EqualTo(original.Id));
                Assert.That(deserialized.FileName, Is.EqualTo(original.FileName));
                Assert.That(deserialized.Description, Is.EqualTo(original.Description));
                Assert.That(deserialized.ContentType, Is.EqualTo(original.ContentType));
                Assert.That(deserialized.FileContent, Is.EqualTo(original.FileContent));
                Assert.That(deserialized.FileSize, Is.EqualTo(original.FileSize));
                Assert.That(deserialized.OriginalFileName, Is.EqualTo(original.OriginalFileName));
                Assert.That(deserialized.CreatedByUserId, Is.EqualTo(original.CreatedByUserId));
                Assert.That(deserialized.DateCreated, Is.EqualTo(original.DateCreated));
                Assert.That(deserialized.DateModified, Is.EqualTo(original.DateModified));
            });
        }
    }
}
