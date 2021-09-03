using ACME.Widget.Company.Common.Models;
using ACME.Widget.Company.Data;
using ACME.Widget.Company.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Widget.Company.Tests.UnitTests
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class ActivityServiceTests
    {
        private ActivityService activityService;
        private Mock<ACMEDbContext> mockDbContext;
        private Mock<DatabaseFacade> mockDatabase;
        private Mock<IDbContextTransaction> mockTransaction;
        private Mock<DbSet<Activity>> mockActivities;
        private Mock<DbSet<Person>> mockPerson;
        private Mock<DbSet<ActivityRegistration>> mockRegistration;
        private Mock<ILogger<ActivityService>> mockLogger;

        [SetUp]
        public void SetUp()
        {
            mockDbContext = new Mock<ACMEDbContext>();
            mockTransaction = new Mock<IDbContextTransaction>();
            mockDatabase = new Mock<DatabaseFacade>(mockDbContext.Object);
            mockLogger = new Mock<ILogger<ActivityService>>();
            mockDbContext.Setup(d => d.Database).Returns(mockDatabase.Object);

            mockActivities = new List<Activity>()
                {
                    new Activity{ Id = 1, Name = "Weekly Jog", RegistrationDeadline = DateTime.MaxValue, EndDate = DateTime.MaxValue }
                }.CreateMock<Activity>();

            mockDbContext.Setup(d => d.Activities).Returns(mockActivities.Object);

            mockPerson = new List<Person>()
                {
                    new Person{ Id = 1, Email = "another@yopmail.com", FirstName = "Joseph", LastName = "Dela Cruz" }
                }.CreateMock<Person>();

            mockDbContext.Setup(d => d.People)
                .Returns(mockPerson.Object);

            mockRegistration = new List<ActivityRegistration>()
                {
                    new ActivityRegistration{ActivityId = 2, PersonId = 1}
                }.CreateMock<ActivityRegistration>();

            mockDbContext.Setup(d => d.ActivityRegistrations)
                .Returns(mockRegistration.Object);

            mockDatabase.Setup(d => d.BeginTransaction()).Returns(mockTransaction.Object);

            activityService = new ActivityService(mockDbContext.Object, mockLogger.Object);
        }

        [Test]
        public async Task RegisterParticipantAsyncTest_NewParticipant()
        {
            var activityId = 1;
            var participant = new Person()
            {
                Email = "test@yopmail.com",
                FirstName = "Joseph",
                LastName = "Dela Cruz"
            };

            var result = await activityService.RegisterParticipantAsync(activityId, string.Empty, participant);

            Assert.That(result.ErrorCode == Common.ErrorCodes.None);
            Assert.IsTrue(result.Result);

            mockDatabase.Verify(db => db.BeginTransaction(), Times.Once);
            mockDbContext.Verify(db => db.SaveChanges(), Times.Exactly(2));
            mockPerson.Verify(db => db.Add(It.IsAny<Person>()), Times.Once());
            mockRegistration.Verify(db => db.Add(It.IsAny<ActivityRegistration>()), Times.Once());
            mockTransaction.Verify(db => db.Commit(), Times.Once);
            mockTransaction.Verify(db => db.Rollback(), Times.Never);
        }

        [Test]
        public async Task RegisterParticipantAsyncTest_ExistingParticipant()
        {
            var activityId = 1;
            var participant = new Person()
            {
                Email = "another@yopmail.com",
                FirstName = "Joseph",
                LastName = "Dela Cruz"
            };

            var result = await activityService.RegisterParticipantAsync(activityId, string.Empty, participant);

            Assert.That(result.ErrorCode == Common.ErrorCodes.None);
            Assert.IsTrue(result.Result);

            mockDatabase.Verify(db => db.BeginTransaction(), Times.Never());
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once());
            mockPerson.Verify(db => db.Add(It.IsAny<Person>()), Times.Never());
            mockRegistration.Verify(db => db.Add(It.IsAny<ActivityRegistration>()), Times.Once());
        }

        [Test]
        public async Task RegisterParticipantAsyncTest_NewParticipantErrors()
        {
            var activityId = 1;
            var participant = new Person()
            {
                Email = "test@yopmail.com",
                FirstName = "Joseph",
                LastName = "Dela Cruz"
            };

            mockDbContext.Setup(d => d.SaveChanges()).Throws(default(Exception));

            var result = await activityService.RegisterParticipantAsync(activityId, string.Empty, participant);

            Assert.That(result.ErrorCode == Common.ErrorCodes.SystemError);
            Assert.False(result.Result);


            mockDatabase.Verify(db => db.BeginTransaction(), Times.Once);
            mockDbContext.Verify(db => db.SaveChanges(), Times.Exactly(1));
            mockTransaction.Verify(db => db.Commit(), Times.Never());
            mockTransaction.Verify(db => db.Rollback(), Times.Once);
            mockLogger.Verify(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once());
        }

        [Test]
        public async Task RegisterParticipantAsyncTest_ExistingParticipantErrors()
        {
            var activityId = 1;
            var participant = new Person()
            {
                Email = "another@yopmail.com",
                FirstName = "Joseph",
                LastName = "Dela Cruz"
            };

            mockDbContext.Setup(d => d.SaveChanges()).Throws(default(Exception));

            var result = await activityService.RegisterParticipantAsync(activityId, string.Empty, participant);

            Assert.That(result.ErrorCode == Common.ErrorCodes.SystemError);
            Assert.False(result.Result);


            mockDatabase.Verify(db => db.BeginTransaction(), Times.Never());
            mockDbContext.Verify(db => db.SaveChanges(), Times.Exactly(1));
            mockTransaction.Verify(db => db.Commit(), Times.Never());
            mockTransaction.Verify(db => db.Rollback(), Times.Never());
            mockLogger.Verify(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once());
        }
    }
}
