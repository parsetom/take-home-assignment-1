using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Widget.Company.API.Models
{
    public class SignUpInformation
    {
        [MaxLength(50), Required]
        public string FirstName { get; set; }
        [MaxLength(50), Required]
        public string LastName { get; set; }
        [MaxLength(50), Required]
        public string Email { get; set; }
        [Required]
        public int ActivityId { get; set; }
        [MaxLength(255)]
        public string Comments { get; set; }
    }
}
