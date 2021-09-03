using ACME.Widget.Company.Common;
using ACME.Widget.Company.Common.Models;
using ACME.Widget.Company.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Widget.Company.Services
{
    public class ActivityService
    {
        protected ACMEDbContext dbContext;
        public ActivityService(ACMEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<ServiceResult<bool>> RegisterParticipant(int activityId, string comment, Person participant)
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
                            serviceResult.ErrorCode = ErrorCodes.SystemError;
                            return Task.FromResult(serviceResult);
                        }
                    }
                }
                else
                {
                    try
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
                    catch (Exception ex)
                    {
                        serviceResult.ErrorCode = ErrorCodes.SystemError;
                        return Task.FromResult(serviceResult);
                    }
                }
            }

            return Task.FromResult(serviceResult);
        }
    }
}
