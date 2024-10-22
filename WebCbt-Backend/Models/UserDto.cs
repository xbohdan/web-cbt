﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebCbt_Backend.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        [EmailAddress]
        public string Login { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public int UserStatus { get; set; }

        public bool Banned { get; set; }
    }
}
