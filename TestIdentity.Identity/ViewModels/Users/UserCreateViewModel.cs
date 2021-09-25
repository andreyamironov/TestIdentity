using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestIdentity.Identity.Core;

namespace TestIdentity.Identity.ViewModels
{
    public class UserCreateViewModel : ViewModelBase
    {

        [Required(ErrorMessage = "Required")]
        [Display(Name = "EMAIL")]
        [RegularExpression("[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}")]
        [EmailAddress(ErrorMessage = "Invalid EMAIL")]
        public string EMail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}