using System.ComponentModel.DataAnnotations;

namespace WebCbt_Backend.Models
{
    public class RegisterUser
    {
        [EmailAddress]
        public string Login { get; set; } = "";

        public string? Password { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; } = "";
    }
}
