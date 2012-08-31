using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using RE = RulesEngine;

namespace RulesEngineEval.Models
{

    public class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String IsValid { get; set; }
        public String Errors { get; set; }
    }

    public class NewUserRegistration
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public String IsValid { get; set; }
    }

    public class Member
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public String IsValid { get; set; }

        public Member() { }
        public Member(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
        
    }

    public class Club
    {
        private readonly List<Member> _members = new List<Member>();
        public string Name { get; set; }
        public Member President { get; set; }
        public String IsValid { get; set; }

        public List<Member> Members
        {
            get { return _members; }
        }

        public Club() { }

        public Club(string name, Member president, params Member[] members)
        {
            this.Name = name;
            this.President = president;
            this._members.AddRange(members);
        }
    }

    public class EmailRule : RE.IRule<string>
    {
        public RE.ValidationResult Validate(string value)
        {
            if (value == null
                || System.Text.RegularExpressions.Regex.IsMatch(value
                    , @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$"
                    , System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                return RE.ValidationResult.Success;
            }
            else
            {
                return RE.ValidationResult.Fail(value);
            }
        }

        public string RuleKind
        {
            get { return this.GetType().Name; }
        }
    }

    public class Employee : Person
    {
        public int EmployeeNumber { get; set; }
    }



    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
