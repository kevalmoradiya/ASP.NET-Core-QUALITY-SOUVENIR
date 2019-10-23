using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualitySouvenirs.Models.Extended
{
    

    public class PUser
    {
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Maximum 20 character")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string UseFName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Maximum 20 character")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string UseLName { get; set; }

        [Display(Name = "Phone No")]
        [DataType(DataType.PhoneNumber)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number required")]
        public string UsePno { get; set; }

        [Display(Name = "Email ID")]
        [StringLength(40, ErrorMessage = "Maximum 20 character")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string UseEmail { get; set; }

        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "Maximum 20 character")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string UsePassword { get; set; }


        public string UseStatus = "Enabled";

        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Maximum 20 character")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required")]
        public string UseAddress { get; set; }
        
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("UsePassword", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
