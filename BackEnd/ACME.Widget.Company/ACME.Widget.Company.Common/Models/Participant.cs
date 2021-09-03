using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class Participant
    {
        public int RegistrationId { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string ActivityName { get; set; }
    }
}
