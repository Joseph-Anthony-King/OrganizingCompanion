using System.Text.Json;
using NUnit.Framework;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;
using OrganizingCompanion.Core.Models;

namespace OrganizingCompanion.Test.TestCases.Models
{
    public class UserShiftShould
    {
        private IUserShift? sut;

        [SetUp]
        public void Setup()
        {
            sut = new UserShift();
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
            Assert.That(((UserShift)sut!).Id, Is.TypeOf<int>());
            Assert.That(((UserShift)sut!).Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void HaveTheExpectedProperties()
        {
            // Arrange and Act

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut?.Id, Is.TypeOf<int>());
                Assert.That(sut!.UserId, Is.TypeOf<int>());
                Assert.That(sut!.UserId, Is.EqualTo(0));
                Assert.That(sut!.ShiftId, Is.TypeOf<int>());
                Assert.That(sut!.ShiftId, Is.EqualTo(0));
                Assert.That(sut!.DateCreated, Is.TypeOf<DateTime>());
                Assert.That(sut!.DateCreated, Is.EqualTo(default(DateTime)));
                Assert.That(sut!.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void HaveAParameterlessConstructor()
        {
            // Arrange and Act
            var userShift = new UserShift();

            // Assert
            Assert.That(userShift, Is.TypeOf<UserShift>());
        }

        [Test, Category("Models")]
        public void HaveAJsonConstructor()
        {
            // Arrange
            var id = 1;
            var userId = 42;
            var shiftId = 123;
            var dateCreated = new DateTime(2024, 1, 15, 8, 0, 0);
            var dateModified = new DateTime(2024, 1, 15, 8, 30, 0);

            // Act
            var userShift = new UserShift(id, userId, shiftId, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userShift, Is.TypeOf<UserShift>());
                Assert.That(userShift.Id, Is.EqualTo(id));
                Assert.That(userShift.UserId, Is.EqualTo(userId));
                Assert.That(userShift.ShiftId, Is.EqualTo(shiftId));
                Assert.That(userShift.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(userShift.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void ThrowNotImplementedExceptionWhenCastingToAnotherType()
        {
            // Arrange and Act

            // Assert
            Assert.Throws<NotImplementedException>(() => sut!.Cast<IUserShift>());
        }

        [Test, Category("Models")]
        public void SerializeToJson()
        {
            // Arrange
            var expectedJson = "{\"id\":0,\"userId\":0,\"shiftId\":0,\"dateCreated\":\"0001-01-01T00:00:00\",\"dateModified\":null}";

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
            var userShift = (UserShift)sut!;
            userShift.Id = 1;
            userShift.UserId = 42;
            userShift.ShiftId = 123;
            var expectedString = "OrganizingCompanion.Core.Models.UserShift.Id:1.UserId:42.ShiftId:123";

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
            var testCreatedDate = DateTime.Now;
            var testModifiedDate = DateTime.Now.AddMinutes(30);

            sut!.UserId = 99;
            sut!.ShiftId = 456;
            sut!.DateCreated = testCreatedDate;
            sut!.DateModified = testModifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut!.UserId, Is.EqualTo(99));
                Assert.That(sut!.ShiftId, Is.EqualTo(456));
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
            domainEntity.Id = 777;

            // Assert
            Assert.That(domainEntity.Id, Is.EqualTo(777));
            Assert.That(((UserShift)sut!).Id, Is.EqualTo(777));
        }

        [Test, Category("Models")]
        public void SerializeToJsonWithCustomValues()
        {
            // Arrange
            var userShift = (UserShift)sut!;
            userShift.Id = 5;
            userShift.UserId = 123;
            userShift.ShiftId = 789;
            userShift.DateCreated = new DateTime(2024, 2, 1, 9, 0, 0);
            userShift.DateModified = new DateTime(2024, 2, 1, 9, 30, 0);

            // Act
            var json = sut!.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain("\"id\":5"));
            Assert.That(json, Does.Contain("\"userId\":123"));
            Assert.That(json, Does.Contain("\"shiftId\":789"));
            Assert.That(json, Does.Contain("\"dateCreated\":\"2024-02-01T09:00:00\""));
            Assert.That(json, Does.Contain("\"dateModified\":\"2024-02-01T09:30:00\""));
        }

        [Test, Category("Models")]
        public void DeserializeFromJsonSuccessfully()
        {
            // Arrange
            var json = "{\"id\":10,\"userId\":456,\"shiftId\":789,\"dateCreated\":\"2024-01-15T10:00:00\",\"dateModified\":\"2024-01-15T10:30:00\"}";

            // Act
            var userShift = JsonSerializer.Deserialize<UserShift>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userShift, Is.Not.Null);
                Assert.That(userShift!.Id, Is.EqualTo(10));
                Assert.That(userShift.UserId, Is.EqualTo(456));
                Assert.That(userShift.ShiftId, Is.EqualTo(789));
                Assert.That(userShift.DateCreated, Is.EqualTo(new DateTime(2024, 1, 15, 10, 0, 0)));
                Assert.That(userShift.DateModified, Is.EqualTo(new DateTime(2024, 1, 15, 10, 30, 0)));
            });
        }

        [Test, Category("Models")]
        public void DeserializeFromJsonWithNullDateModified()
        {
            // Arrange
            var json = "{\"id\":15,\"userId\":789,\"shiftId\":321,\"dateCreated\":\"2024-03-01T08:00:00\",\"dateModified\":null}";

            // Act
            var userShift = JsonSerializer.Deserialize<UserShift>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userShift, Is.Not.Null);
                Assert.That(userShift!.Id, Is.EqualTo(15));
                Assert.That(userShift.UserId, Is.EqualTo(789));
                Assert.That(userShift.ShiftId, Is.EqualTo(321));
                Assert.That(userShift.DateCreated, Is.EqualTo(new DateTime(2024, 3, 1, 8, 0, 0)));
                Assert.That(userShift.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void RoundTripSerializationPreservesData()
        {
            // Arrange
            var originalUserShift = new UserShift();
            var userShift = (UserShift)originalUserShift;
            userShift.Id = 25;
            userShift.UserId = 333;
            userShift.ShiftId = 666;
            userShift.DateCreated = new DateTime(2024, 4, 1, 7, 0, 0);
            userShift.DateModified = new DateTime(2024, 4, 1, 7, 15, 0);

            // Act
            var json = originalUserShift.ToJson();
            var deserializedUserShift = JsonSerializer.Deserialize<UserShift>(json)!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedUserShift, Is.Not.Null);
                Assert.That(deserializedUserShift!.Id, Is.EqualTo(userShift.Id));
                Assert.That(deserializedUserShift.UserId, Is.EqualTo(userShift.UserId));
                Assert.That(deserializedUserShift.ShiftId, Is.EqualTo(userShift.ShiftId));
                Assert.That(deserializedUserShift.DateCreated, Is.EqualTo(userShift.DateCreated));
                Assert.That(deserializedUserShift.DateModified, Is.EqualTo(userShift.DateModified));
            });
        }

        [Test, Category("Models")]
        public void ToStringWithZeroValues()
        {
            // Arrange
            var expectedString = "OrganizingCompanion.Core.Models.UserShift.Id:0.UserId:0.ShiftId:0";

            // Act
            var str = sut!.ToString();

            // Assert
            Assert.That(str, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void HandleNullDateModifiedCorrectly()
        {
            // Arrange
            var userShift = (UserShift)sut!;
            userShift.DateModified = null;

            // Act & Assert
            Assert.That(userShift.DateModified, Is.Null);
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
            Assert.That(((UserShift)sut!).DateModified, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceImplementationWorksCorrectly()
        {
            // Arrange
            var userShift = new UserShift();
            var iUserShift = (IUserShift)userShift;
            var iDomainEntity = (IDomainEntity)userShift;
            var testCreatedDate = DateTime.Now;
            var testModifiedDate = DateTime.Now.AddMinutes(15);

            // Act
            iDomainEntity.Id = 888;
            iUserShift.UserId = 999;
            iUserShift.ShiftId = 111;
            iDomainEntity.DateCreated = testCreatedDate;
            iDomainEntity.DateModified = testModifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userShift.Id, Is.EqualTo(888));
                Assert.That(userShift.UserId, Is.EqualTo(999));
                Assert.That(userShift.ShiftId, Is.EqualTo(111));
                Assert.That(userShift.DateCreated, Is.EqualTo(testCreatedDate));
                Assert.That(userShift.DateModified, Is.EqualTo(testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void RoundTripSerializationWithNullDateModified()
        {
            // Arrange
            var originalUserShift = new UserShift();
            var userShift = (UserShift)originalUserShift;
            userShift.Id = 50;
            userShift.UserId = 200;
            userShift.ShiftId = 300;
            userShift.DateCreated = new DateTime(2024, 5, 1, 6, 0, 0);
            userShift.DateModified = null;

            // Act
            var json = originalUserShift.ToJson();
            var deserializedUserShift = JsonSerializer.Deserialize<UserShift>(json)!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedUserShift, Is.Not.Null);
                Assert.That(deserializedUserShift!.Id, Is.EqualTo(userShift.Id));
                Assert.That(deserializedUserShift.UserId, Is.EqualTo(userShift.UserId));
                Assert.That(deserializedUserShift.ShiftId, Is.EqualTo(userShift.ShiftId));
                Assert.That(deserializedUserShift.DateCreated, Is.EqualTo(userShift.DateCreated));
                Assert.That(deserializedUserShift.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonPropertyNamesAreCorrect()
        {
            // Arrange
            var userShift = new UserShift();
            userShift.Id = 100;
            userShift.UserId = 200;
            userShift.ShiftId = 300;
            userShift.DateCreated = new DateTime(2024, 6, 1, 12, 0, 0);

            // Act
            var json = userShift.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Contain("\"id\":"));
                Assert.That(json, Does.Contain("\"userId\":"));
                Assert.That(json, Does.Contain("\"shiftId\":"));
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Not.Contain("\"Id\":"));
                Assert.That(json, Does.Not.Contain("\"UserId\":"));
                Assert.That(json, Does.Not.Contain("\"ShiftId\":"));
                Assert.That(json, Does.Not.Contain("\"DateCreated\":"));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructorWithNullDateModified()
        {
            // Arrange
            var id = 75;
            var userId = 150;
            var shiftId = 225;
            var dateCreated = new DateTime(2024, 7, 1, 14, 0, 0);
            DateTime? dateModified = null;

            // Act
            var userShift = new UserShift(id, userId, shiftId, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userShift.Id, Is.EqualTo(id));
                Assert.That(userShift.UserId, Is.EqualTo(userId));
                Assert.That(userShift.ShiftId, Is.EqualTo(shiftId));
                Assert.That(userShift.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(userShift.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void SerializationIgnoresCycles()
        {
            // Arrange
            var userShift = (UserShift)sut!;
            userShift.Id = 1;
            userShift.UserId = 2;
            userShift.ShiftId = 3;

            // Act & Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() => userShift.ToJson());
        }

        [Test, Category("Models")]
        public void DefaultConstructorInitializesPropertiesCorrectly()
        {
            // Arrange & Act
            var userShift = new UserShift();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userShift.Id, Is.EqualTo(0));
                Assert.That(userShift.UserId, Is.EqualTo(0));
                Assert.That(userShift.ShiftId, Is.EqualTo(0));
                Assert.That(userShift.DateCreated, Is.EqualTo(default(DateTime)));
                Assert.That(userShift.DateModified, Is.Null);
            });
        }
    }
}