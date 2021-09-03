using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class ActivityRegistration : BaseEntity
    {
        public Person Person { get; set; }
        public Activity Activity { get; set; }
        public int PersonId { get; set; }
        public int ActivityId { get; set; }
        public string Comments { get; set; }
    }
}
