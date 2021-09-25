using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Core;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class UserEditViewModel
    {
        [HiddenInput]
        public string Id { get; set; }
        [HiddenInput]
        public string OriginalId { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Почта")]
        [RegularExpression("[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}")]
        [EmailAddress(ErrorMessage = "Недопустимый адрес почты.")]
        public string EMail { get; set; }   
        public bool EmailConfirmed { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
