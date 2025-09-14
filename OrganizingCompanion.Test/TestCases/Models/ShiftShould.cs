using System.Text.Json;
using NUnit.Framework;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;
using OrganizingCompanion.Core.Models;

namespace OrganizingCompanion.Test.TestCases.Models
{
    public class ShiftShould
    {
        private IShift? sut;

        [SetUp]
        public void Setup()
        {
            sut = new Shift();
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
            Assert.That(((Shift)sut!).Id, Is.TypeOf<int>());
            Assert.That(((Shift)sut!).Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void HaveTheExpectedProperties()
        {
            // Arrange and Act

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut?.Id, Is.TypeOf<int>());
                Assert.That(sut!.StartDateTime, Is.TypeOf<DateTime>());
                Assert.That(sut!.StartDateTime, Is.EqualTo(default(DateTime)));
                Assert.That(sut!.EndDateTime, Is.TypeOf<DateTime>());
                Assert.That(sut!.EndDateTime, Is.EqualTo(default(DateTime)));
                Assert.That(sut!.UserId, Is.TypeOf<int>());
                Assert.That(sut!.UserId, Is.EqualTo(0));
                Assert.That(sut!.User, Is.Null);
                Assert.That(sut!.DateCreated, Is.TypeOf<DateTime>());
                Assert.That(sut!.DateCreated, Is.EqualTo(default(DateTime)));
                Assert.That(sut!.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void HaveAParameterlessConstructor()
        {
            // Arrange and Act
            var shift = new Shift();
            // Assert
            Assert.That(shift, Is.TypeOf<Shift>());
        }

        [Test, Category("Models")]
        public void ThrowNotImplementedExceptionWhenCastingToAnotherType()
        {
            // Arrange and Act
            // Assert
            Assert.Throws<NotImplementedException>(() => sut!.Cast<IShift>());
        }

        [Test, Category("Models")]
        public void SerializeToJson()
        {
            // Arrange
            var expectedJson = "{\"id\":0,\"startDateTime\":\"0001-01-01T00:00:00\",\"endDateTime\":\"0001-01-01T00:00:00\",\"userId\":0,\"user\":null,\"dateCreated\":\"0001-01-01T00:00:00\"}";
            // Act
            var json = sut!.ToJson();
            // Assert
            Assert.That(json, Is.TypeOf<string>());
            Assert.That(json, Is.EqualTo(expectedJson));
        }

        [Test, Category("Models")]
        public void OverrideToString()
        {
            // Arrange
            var shift = (Shift)sut!;
            shift.Id = 1;
            shift.StartDateTime = new DateTime(2024, 1, 15, 9, 0, 0, DateTimeKind.Utc);
            shift.EndDateTime = new DateTime(2024, 1, 15, 17, 0, 0, DateTimeKind.Utc);
            var expectedString = "OrganizingCompanion.Core.Models.Shift.Id:1.StartDateTime:1/15/2024 9:00:00 AM.EndDateTime:1/15/2024 5:00:00 PM.UserName:1/15/2024 5:00:00 PM";

            // Act
            var str = sut!.ToString();

            // Assert
            Assert.That(str, Is.TypeOf<string>());
            Assert.That(str, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void AllowSettingPropertiesThroughInterface()
        {
            // Arrange & Act
            var testStartDateTime = new DateTime(2024, 2, 1, 8, 0, 0);
            var testEndDateTime = new DateTime(2024, 2, 1, 16, 0, 0);
            var testCreatedDate = DateTime.Now;
            var testModifiedDate = DateTime.Now.AddMinutes(30);
            var testUser = new User();

            sut!.StartDateTime = testStartDateTime;
            sut!.EndDateTime = testEndDateTime;
            sut!.UserId = 42;
            sut!.User = testUser;
            sut!.DateCreated = testCreatedDate;
            sut!.DateModified = testModifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut!.StartDateTime, Is.EqualTo(testStartDateTime));
                Assert.That(sut!.EndDateTime, Is.EqualTo(testEndDateTime));
                Assert.That(sut!.UserId, Is.EqualTo(42));
                Assert.That(sut!.User, Is.EqualTo(testUser));
                Assert.That(sut!.DateCreated, Is.EqualTo(testCreatedDate));
                Assert.That(sut!.DateModified, Is.EqualTo(testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void AllowSettingIdThroughDomainEntityInterface()
        {
            // Arrange
            var domainEntity = (IDomainEntity)sut!;

            // Act
            domainEntity.Id = 99;

            // Assert
            Assert.That(domainEntity.Id, Is.EqualTo(99));
            Assert.That(((Shift)sut!).Id, Is.EqualTo(99));
        }

        [Test, Category("Models")]
        public void SerializeToJsonWithUser()
        {
            // Arrange
            var testUser = new User();
            testUser.Id = 123;
            testUser.UserName = "testuser";
            var shift = (Shift)sut!;
            shift.Id = 1;
            shift.StartDateTime = new DateTime(2024, 1, 1, 9, 0, 0);
            shift.EndDateTime = new DateTime(2024, 1, 1, 17, 0, 0);
            shift.UserId = 123;
            shift.User = testUser;
            shift.DateCreated = new DateTime(2024, 1, 1, 8, 0, 0);

            // Act
            var json = sut!.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain("\"id\":1"));
            Assert.That(json, Does.Contain("\"userId\":123"));
            Assert.That(json, Does.Contain("\"user\":"));
        }

        [Test, Category("Models")]
        public void DeserializeFromJsonSuccessfully()
        {
            // Arrange
            var json = "{\"id\":5,\"startDateTime\":\"2024-01-15T09:00:00\",\"endDateTime\":\"2024-01-15T17:00:00\",\"userId\":42,\"user\":null,\"dateCreated\":\"2024-01-15T08:00:00\"}";

            // Act
            var shift = JsonSerializer.Deserialize<Shift>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(shift, Is.Not.Null);
                Assert.That(shift!.Id, Is.EqualTo(5));
                Assert.That(shift.StartDateTime, Is.EqualTo(new DateTime(2024, 1, 15, 9, 0, 0)));
                Assert.That(shift.EndDateTime, Is.EqualTo(new DateTime(2024, 1, 15, 17, 0, 0)));
                Assert.That(shift.UserId, Is.EqualTo(42));
                Assert.That(shift.User, Is.Null);
                Assert.That(shift.DateCreated, Is.EqualTo(new DateTime(2024, 1, 15, 8, 0, 0)));
                Assert.That(shift.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DeserializeFromJsonWithUser()
        {
            // Arrange
            var json = "{\"id\":6,\"startDateTime\":\"2024-02-01T10:00:00\",\"endDateTime\":\"2024-02-01T18:00:00\",\"userId\":99,\"user\":{\"id\":99,\"username\":\"john\",\"firstName\":\"John\",\"lastName\":\"Doe\",\"email\":\"john@example.com\",\"phone\":\"555-1234\",\"isOrganizer\":false,\"dateCreated\":\"2024-01-01T00:00:00\",\"dateModified\":\"2024-01-01T00:00:00\"},\"dateCreated\":\"2024-02-01T09:00:00\",\"dateModified\":\"2024-02-01T09:30:00\"}";

            // Act
            var shift = JsonSerializer.Deserialize<Shift>(json)!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(shift, Is.Not.Null);
                Assert.That(shift!.Id, Is.EqualTo(6));
                Assert.That(shift.StartDateTime, Is.EqualTo(new DateTime(2024, 2, 1, 10, 0, 0)));
                Assert.That(shift.EndDateTime, Is.EqualTo(new DateTime(2024, 2, 1, 18, 0, 0)));
                Assert.That(shift.UserId, Is.EqualTo(99));
                Assert.That(shift.User, Is.Not.Null);
                Assert.That(shift.User!.Id, Is.EqualTo(99));
                Assert.That(shift.User!.UserName, Is.EqualTo("john"));
                Assert.That(shift.DateCreated, Is.EqualTo(new DateTime(2024, 2, 1, 9, 0, 0)));
                Assert.That(shift.DateModified, Is.EqualTo(new DateTime(2024, 2, 1, 9, 30, 0)));
            });
        }

        [Test, Category("Models")]
        public void RoundTripSerializationPreservesData()
        {
            // Arrange
            var originalShift = new Shift();
            var shift = (Shift)originalShift;
            shift.Id = 123;
            shift.StartDateTime = new DateTime(2024, 3, 1, 8, 0, 0);
            shift.EndDateTime = new DateTime(2024, 3, 1, 16, 0, 0);
            shift.UserId = 456;
            shift.DateCreated = new DateTime(2024, 2, 28, 12, 0, 0);
            shift.DateModified = new DateTime(2024, 2, 28, 12, 30, 0);

            // Act
            var json = originalShift.ToJson();
            var deserializedShift = JsonSerializer.Deserialize<Shift>(json)!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedShift, Is.Not.Null);
                Assert.That(deserializedShift!.Id, Is.EqualTo(shift.Id));
                Assert.That(deserializedShift.StartDateTime, Is.EqualTo(shift.StartDateTime));
                Assert.That(deserializedShift.EndDateTime, Is.EqualTo(shift.EndDateTime));
                Assert.That(deserializedShift.UserId, Is.EqualTo(shift.UserId));
                Assert.That(deserializedShift.User, Is.EqualTo(shift.User));
                Assert.That(deserializedShift.DateCreated, Is.EqualTo(shift.DateCreated));
                Assert.That(deserializedShift.DateModified, Is.EqualTo(shift.DateModified));
            });
        }

        [Test, Category("Models")]
        public void ToStringWithZeroValues()
        {
            // Arrange
            var expectedString = "OrganizingCompanion.Core.Models.Shift.Id:0.StartDateTime:1/1/0001 12:00:00 AM.EndDateTime:1/1/0001 12:00:00 AM.UserName:1/1/0001 12:00:00 AM";

            // Act
            var str = sut!.ToString();

            // Assert
            Assert.That(str, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void HandleNullDateModifiedCorrectly()
        {
            // Arrange
            var shift = (Shift)sut!;
            shift.DateModified = null;

            // Act & Assert
            Assert.That(shift.DateModified, Is.Null);
            Assert.That(sut!.DateModified, Is.Null);
        }

        [Test, Category("Models")]
        public void AllowSettingDateModifiedThroughInterface()
        {
            // Arrange
            var testDate = DateTime.Now;

            // Act
            sut!.DateModified = testDate;

            // Assert
            Assert.That(sut!.DateModified, Is.EqualTo(testDate));
            Assert.That(((Shift)sut!).DateModified, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceImplementationWorksCorrectly()
        {
            // Arrange
            var shift = new Shift();
            var iShift = (IShift)shift;
            var iDomainEntity = (IDomainEntity)shift;
            var testStartDate = new DateTime(2024, 4, 1, 9, 0, 0);
            var testEndDate = new DateTime(2024, 4, 1, 17, 0, 0);
            var testCreatedDate = DateTime.Now;
            var testModifiedDate = DateTime.Now.AddMinutes(15);
            var testUser = new User();

            // Act
            iDomainEntity.Id = 777;
            iShift.StartDateTime = testStartDate;
            iShift.EndDateTime = testEndDate;
            iShift.UserId = 888;
            iShift.User = testUser;
            iDomainEntity.DateCreated = testCreatedDate;
            iDomainEntity.DateModified = testModifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(shift.Id, Is.EqualTo(777));
                Assert.That(shift.StartDateTime, Is.EqualTo(testStartDate));
                Assert.That(shift.EndDateTime, Is.EqualTo(testEndDate));
                Assert.That(shift.UserId, Is.EqualTo(888));
                Assert.That(shift.User, Is.EqualTo(testUser));
                Assert.That(shift.DateCreated, Is.EqualTo(testCreatedDate));
                Assert.That(shift.DateModified, Is.EqualTo(testModifiedDate));
            });
        }
    }
}