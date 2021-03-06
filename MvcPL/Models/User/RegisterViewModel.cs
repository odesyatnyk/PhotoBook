﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.User
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your e-mail")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Display(Name = "Enter your name")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"(\w+)", ErrorMessage = "Invalid name")]
        [StringLength(30, ErrorMessage = "The name must contain at lest {2} characters", MinimumLength = 4)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }
    }
}