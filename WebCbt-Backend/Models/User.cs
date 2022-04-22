using System;
using System.Collections.Generic;

namespace WebCbt_Backend.Models
{
    public partial class User
    {
        public string UserId { get; set; } = null!;
        public int? Age { get; set; }
        public string Gender { get; set; } = null!;
        public int UserStatus { get; set; }
        public bool Banned { get; set; }
    }
}
