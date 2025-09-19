using System.Text.Json;
using NUnit.Framework;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;
using OrganizingCompanion.Core.Models;

namespace OrganizingCompanion.Test.TestCases.Models
{
    public class UserShould
    {
        private IUser? sut;

        [SetUp]
        public void Setup()
        {
            sut = new User();
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
            Assert.That(((User)sut!).Id, Is.TypeOf<int>());
            Assert.That(((User)sut!).Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void HaveTheExpectedProperties()
        {
            // Arrange and Act

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut?.Id, Is.TypeOf<int>());
                Assert.That(sut!.UserName, Is.TypeOf<string>());
                Assert.That(sut!.UserName, Is.EqualTo(string.Empty));
                Assert.That(sut!.FirstName, Is.Null);
                Assert.That(sut!.LastName, Is.Null);
                Assert.That(sut!.Email, Is.TypeOf<string>());
                Assert.That(sut!.Email, Is.EqualTo(string.Empty));
                Assert.That(sut!.Phone, Is.TypeOf<string>());
                Assert.That(sut!.Phone, Is.EqualTo(string.Empty));
                Assert.That(sut!.IsOrganizer, Is.TypeOf<bool>());
                Assert.That(sut!.IsOrganizer, Is.False);
                Assert.That(sut!.DateCreated, Is.TypeOf<DateTime>());
                Assert.That(sut!.DateCreated, Is.EqualTo(default(DateTime)));
                Assert.That(sut!.DateModified, Is.TypeOf<DateTime>());
                Assert.That(sut!.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void HaveAParameterlessConstructor()
        {
            // Arrange and Act
            var user = new User();
            // Assert
            Assert.That(user, Is.TypeOf<User>());
        }

        [Test, Category("Models")]
        public void HaveAJsonConstructor()
        {
            // Arrange and Act
            var now = DateTime.Now;
            var createdDate = now.Subtract(new TimeSpan(1, 0, 0, 0));
            sut = new User(
                1,
                "userName",
                "firstName",
                "lastName",
                "email",
                "phone",
                "password",
                true,
                createdDate,
                now);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut, Is.TypeOf<User>());
                Assert.That(((User)sut).Id, Is.EqualTo(1));
                Assert.That(sut!.UserName, Is.EqualTo("userName"));
                Assert.That(sut!.FirstName, Is.EqualTo("firstName"));
                Assert.That(sut!.LastName, Is.EqualTo("lastName"));
                Assert.That(sut!.Email, Is.EqualTo("email"));
                Assert.That(sut!.Phone, Is.EqualTo("phone"));
                Assert.That(sut!.IsOrganizer, Is.True);
                Assert.That(sut!.DateCreated, Is.EqualTo(createdDate));
                Assert.That(sut!.DateModified, Is.EqualTo(now));
            });
        }

        [Test, Category("Models")]
        public void ThrowNotImplementedExceptionWhenCastingToAnotherType()
        {
            // Arrange and Act
            // Assert
            Assert.Throws<NotImplementedException>(() => sut!.Cast<IUser>());
        }

        [Test, Category("Models")]
        public void SerializeToJson()
        {
            // Arrange
            var expectedJson = "{\"id\":0,\"username\":\"\",\"firstName\":null,\"lastName\":null,\"email\":\"\",\"phone\":\"\",\"shifts\":[],\"isOrganizer\":false,\"dateCreated\":\"0001-01-01T00:00:00\",\"dateModified\":\"0001-01-01T00:00:00\"}";
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
            var expectedString = "OrganizingCompanion.Core.Models.User.Id:1.UserName:JohnDoe";
            ((User)sut!).Id = 1;
            sut!.UserName = "JohnDoe";
            // Act
            var str = sut!.ToString();
            // Assert
            Assert.That(str, Is.TypeOf<string>());
            Assert.That(str, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void ReturnEmptyStringForNullPasswordThroughInterface()
        {
            // Arrange
            sut!.Password = null;

            // Act
            var password = sut!.Password;

            // Assert
            Assert.That(password, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void ReturnActualPasswordWhenNotNull()
        {
            // Arrange
            var testPassword = "testPassword123";
            sut!.Password = testPassword;

            // Act
            var password = sut!.Password;

            // Assert
            Assert.That(password, Is.EqualTo(testPassword));
        }

        [Test, Category("Models")]
        public void SerializeToJsonWithNullPassword()
        {
            // Arrange
            sut!.Password = null;

            // Act
            var json = sut!.ToJson();

            // Assert
            Assert.That(json, Does.Not.Contain("password"));
        }

        [Test, Category("Models")]
        public void SerializeToJsonWithNonNullPassword()
        {
            // Arrange
            sut!.Password = "testPassword";

            // Act
            var json = sut!.ToJson();

            // Assert
            Assert.That(json, Does.Contain("\"password\":\"testPassword\""));
        }

        [Test, Category("Models")]
        public void ScrubPasswordSetsPasswordToNull()
        {
            // Arrange
            sut!.Password = "sensitivePassword";
            // Act
            sut!.ScrubPassword();
            // Assert
            Assert.That(sut!.Password, Is.EqualTo(string.Empty));
            Assert.That(((User)sut).Password, Is.Null);
        }

        [Test, Category("Models")]
        public void DeserializeFromJsonSuccessfully()
        {
            // Arrange
            var json = "{\"id\":5,\"username\":\"testUser\",\"firstName\":\"John\",\"lastName\":\"Doe\",\"email\":\"john@example.com\",\"phone\":\"123-456-7890\",\"password\":\"secret\",\"isOrganizer\":true,\"dateCreated\":\"2023-01-01T10:00:00\",\"dateModified\":\"2023-01-02T15:30:00\"}";

            // Act
            var user = JsonSerializer.Deserialize<User>(json)!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user, Is.Not.Null);
                Assert.That(user.Id, Is.EqualTo(5));
                Assert.That(user.UserName, Is.EqualTo("testUser"));
                Assert.That(user.FirstName, Is.EqualTo("John"));
                Assert.That(user.LastName, Is.EqualTo("Doe"));
                Assert.That(user.Email, Is.EqualTo("john@example.com"));
                Assert.That(user.Phone, Is.EqualTo("123-456-7890"));
                Assert.That(user.IsOrganizer, Is.True);
                Assert.That(user.DateCreated, Is.EqualTo(new DateTime(2023, 1, 1, 10, 0, 0)));
                Assert.That(user.DateModified, Is.EqualTo(new DateTime(2023, 1, 2, 15, 30, 0)));
            });
        }

        [Test, Category("Models")]
        public void AllowSettingPropertiesThroughInterface()
        {
            // Arrange & Act
            sut!.UserName = "newUserName";
            sut!.FirstName = "NewFirst";
            sut!.LastName = "NewLast";
            sut!.Email = "new@email.com";
            sut!.Phone = "987-654-3210";
            sut!.Password = "newPassword";
            sut!.IsOrganizer = true;
            var testDate = DateTime.Now;
            sut!.DateCreated = testDate;
            sut!.DateModified = testDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut!.UserName, Is.EqualTo("newUserName"));
                Assert.That(sut!.FirstName, Is.EqualTo("NewFirst"));
                Assert.That(sut!.LastName, Is.EqualTo("NewLast"));
                Assert.That(sut!.Email, Is.EqualTo("new@email.com"));
                Assert.That(sut!.Phone, Is.EqualTo("987-654-3210"));
                Assert.That(sut!.Password, Is.EqualTo("newPassword"));
                Assert.That(sut!.IsOrganizer, Is.True);
                Assert.That(sut!.DateCreated, Is.EqualTo(testDate));
                Assert.That(sut!.DateModified, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void AllowSettingIdThroughDomainEntityInterface()
        {
            // Arrange
            var domainEntity = (IDomainEntity)sut!;

            // Act
            domainEntity.Id = 42;

            // Assert
            Assert.That(domainEntity.Id, Is.EqualTo(42));
            Assert.That(((User)sut!).Id, Is.EqualTo(42));
        }

        [Test, Category("Models")]
        public void ToStringWithEmptyUserName()
        {
            // Arrange
            ((User)sut!).Id = 10;
            sut!.UserName = string.Empty;
            var expectedString = "OrganizingCompanion.Core.Models.User.Id:10.UserName:";

            // Act
            var str = sut!.ToString();

            // Assert
            Assert.That(str, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void ToStringWithZeroId()
        {
            // Arrange
            sut!.UserName = "TestUser";
            var expectedString = "OrganizingCompanion.Core.Models.User.Id:0.UserName:TestUser";

            // Act
            var str = sut!.ToString();

            // Assert
            Assert.That(str, Is.EqualTo(expectedString));
        }

        [Test, Category("Models")]
        public void HandleNullFirstAndLastNameInJsonConstructor()
        {
            // Arrange & Act
            var user = new User(
                1,
                "testUser",
                null,
                null,
                "test@email.com",
                "123-456-7890",
                "password",
                false,
                DateTime.Now,
                DateTime.Now);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user.FirstName, Is.Null);
                Assert.That(user.LastName, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void HandleNullPasswordInJsonConstructor()
        {
            // Arrange & Act
            var user = new User(
                1,
                "testUser",
                "First",
                "Last",
                "test@email.com",
                "123-456-7890",
                null,
                false,
                DateTime.Now,
                DateTime.Now);

            // Assert
            Assert.That(((User)user).Password, Is.Null);
            Assert.That(((IUser)user).Password, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void RoundTripSerializationPreservesData()
        {
            // Arrange
            var originalUser = new User(
                123,
                "originalUser",
                "Original",
                "User",
                "original@test.com",
                "555-0123",
                "secretPassword",
                true,
                new DateTime(2023, 6, 15, 9, 30, 0),
                new DateTime(2023, 6, 16, 14, 45, 0));

            // Act
            var json = originalUser.ToJson();
            var deserializedUser = JsonSerializer.Deserialize<User>(json)!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedUser, Is.Not.Null);
                Assert.That(deserializedUser.Id, Is.EqualTo(originalUser.Id));
                Assert.That(deserializedUser.UserName, Is.EqualTo(originalUser.UserName));
                Assert.That(deserializedUser.FirstName, Is.EqualTo(originalUser.FirstName));
                Assert.That(deserializedUser.LastName, Is.EqualTo(originalUser.LastName));
                Assert.That(deserializedUser.Email, Is.EqualTo(originalUser.Email));
                Assert.That(deserializedUser.Phone, Is.EqualTo(originalUser.Phone));
                Assert.That(deserializedUser.IsOrganizer, Is.EqualTo(originalUser.IsOrganizer));
                Assert.That(deserializedUser.DateCreated, Is.EqualTo(originalUser.DateCreated));
                Assert.That(deserializedUser.DateModified, Is.EqualTo(originalUser.DateModified));
            });
        }
    }
}
