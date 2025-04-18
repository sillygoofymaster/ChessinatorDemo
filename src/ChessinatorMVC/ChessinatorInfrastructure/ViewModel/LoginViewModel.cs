using ChessinatorInfrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace ChessinatorInfrastructure.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }

    }
}