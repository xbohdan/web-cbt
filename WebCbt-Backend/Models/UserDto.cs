using System.Text.Json.Serialization;

namespace WebCbt_Backend.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
    }
}
