using System.ComponentModel.DataAnnotations;

namespace WebCbt_Backend.Models
{
    public class LoginUser
    {
        [EmailAddress]
        public string Login { get; set; } = "";

        public string Password { get; set; } = "";
    }
}
