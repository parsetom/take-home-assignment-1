using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public List<ActivityRegistration> Registrations { get; set; }
    }
}
