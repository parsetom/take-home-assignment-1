using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
