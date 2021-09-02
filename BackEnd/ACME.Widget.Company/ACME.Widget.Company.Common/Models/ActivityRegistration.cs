using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class ActivityRegistration : BaseEntity
    {
        public int PersonId { get; set; }
        public int ActivityId { get; set; }
        public string Comments { get; set; }
    }
}
