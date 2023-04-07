﻿using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class SignInViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}