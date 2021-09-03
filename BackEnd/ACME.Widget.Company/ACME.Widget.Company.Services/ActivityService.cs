using ACME.Widget.Company.Common;
using ACME.Widget.Company.Common.Models;
using ACME.Widget.Company.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Widget.Company.Services
{
    public class ActivityService
    {
        protected ACMEDbContext dbContext;
        protected ILogger<ActivityService> logger;

        public ActivityService(ACMEDbContext dbContext, ILogger<ActivityService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public Task<ServiceResult<IEnumerable<Activity>>> GetActivitiesAsync(string keyword)
        {
            var serviceResult = new ServiceResult<IEnumerable<Activity>>();

            try
            {
                serviceResult.Result = dbContext.Activities.Where(a => a.Name.Contains(keyword ?? string.Empty)).ToList();
            }
            catch (Exception ex)
            {
                serviceResult.ErrorCode = ErrorCodes.SystemError;
                logger.LogError(ex, "Error GetActivitiesAsync");
            }

            return Task.FromResult(serviceResult);
        }

        public Task<ServiceResult<IEnumerable<Participant>>> GetParticipantsAsync(int activityId)
        {
            var serviceResult = new ServiceResult<IEnumerable<Participant>>();

            try
            {
                var registrations = dbContext.ActivityRegistrations.Where(r => r.ActivityId == activityId)
                .Include(r => r.Person)
                .Include(r => r.Activity);

                var participants = registrations.Select(r => new Participant
                {
                    RegistrationId = r.Id,
                    PersonId = r.PersonId,
                    Name = $"{r.Person.FirstName} {r.Person.LastName}",
                    Email = r.Person.Email,
                    ActivityName = r.Activity.Name,
                    Comments = r.Comments
                });

                serviceResult.Result = participants.ToList();
            }
            catch (Exception ex)
            {
                serviceResult.ErrorCode = ErrorCodes.SystemError;
                logger.LogError(ex, "Error GetParticipantsAsync");
            }

            return Task.FromResult(serviceResult);
        }

        public Task<ServiceResult<bool>> RegisterParticipantAsync(int activityId, string comment, Person participant)
        {
            var serviceResult = new ServiceResult<bool>();

            var activity = dbContext.Activities.FirstOrDefault(a => a.Id == activityId);

            if (activity == null)
            {
                serviceResult.ErrorCode = ErrorCodes.ActivityNotFound;
            }
            else if (activity.RegistrationDeadline < DateTime.Now)
            {
                serviceResult.ErrorCode = ErrorCodes.LateRegistration;
            }
            else if (activity.EndDate < DateTime.Now)
            {
                serviceResult.ErrorCode = ErrorCodes.EventEnded;
            }
            else
            {
                var person = dbContext.People.FirstOrDefault(p => p.Email == participant.Email);

                if (person == null)
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            dbContext.People.Add(participant);

                            dbContext.SaveChanges();

                            dbContext.ActivityRegistrations.Add(new ActivityRegistration
                            {
                                ActivityId = activityId,
                                PersonId = participant.Id,
                                Comments = comment
                            });

                            dbContext.SaveChanges();

                            transaction.Commit();
                            serviceResult.Result = true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            logger.LogError(ex, $"Email: {participant.Email}");
                            serviceResult.ErrorCode = ErrorCodes.SystemError;
                            return Task.FromResult(serviceResult);
                        }
                    }
                }
                else
                {
                    try
                    {
                        var isAlreadyRegistered = dbContext.ActivityRegistrations.Any(a => a.PersonId == participant.Id && a.ActivityId == activityId);

                        if (isAlreadyRegistered)
                        {
                            serviceResult.ErrorCode = ErrorCodes.AlreadyInEvent;
                        }
                        else
                        {
                            dbContext.ActivityRegistrations.Add(new ActivityRegistration
                            {
                                ActivityId = activityId,
                                PersonId = person.Id,
                                Comments = comment
                            });
                            dbContext.SaveChanges();
                            serviceResult.Result = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Email: {participant.Email}");
                        serviceResult.ErrorCode = ErrorCodes.SystemError;
                        return Task.FromResult(serviceResult);
                    }
                }
            }

            return Task.FromResult(serviceResult);
        }
    }
}
