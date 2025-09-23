using NUnit.Framework;
using OrganizingCompanion.Core.Interfaces.Models;
using OrganizingCompanion.Core.Interfaces.Models.DomainEntities;
using OrganizingCompanion.Scheduler.Interfaces.Models;
using OrganizingCompanion.Scheduler.Models;

namespace OrganizingCompanion.Test.TestCases.Models.Scheduler
{
    public class RosterShould
    {
        private IRoster? sut;

        [SetUp]
        public void Setup()
        {
            sut = new Roster();
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
            Assert.That(((Roster)sut!).Id, Is.TypeOf<int>());
            Assert.That(((Roster)sut!).Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void HaveTheExpectedProperties()
        {
            // Arrange and Act
            // Assert
            Assert.That(sut!.Users, Is.TypeOf<List<IUser>>());
            Assert.That(sut.Users, Is.Empty);
            Assert.That(sut.StartDateTime, Is.TypeOf<DateTime>());
            Assert.That(sut.StartDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(sut.EndDateTime, Is.TypeOf<DateTime>());
            Assert.That(sut.EndDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(sut.DateCreated, Is.TypeOf<DateTime>());
            Assert.That(sut.DateCreated, Is.EqualTo(default(DateTime)));
            Assert.That(sut.DateModified, Is.Null);
        }
    }
}
