using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportingDashboard.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string pass { get; set; }
    }
}
