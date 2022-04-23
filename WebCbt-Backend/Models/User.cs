using System;
using System.Collections.Generic;

namespace WebCbt_Backend.Models
{
    public partial class User
    {
        public string Id { get; set; } = "";

        public int UserId { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; } = "";

        public int UserStatus { get; set; }

        public bool Banned { get; set; }
    }
}
