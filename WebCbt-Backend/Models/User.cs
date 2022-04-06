namespace WebCbt_Backend.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Login { get; set; } = "";

        public string Password { get; set; } = "";

        public int Age { get; set; }

        public string Gender { get; set; } = "";

        public int UserStatus { get; set; }

        public bool Banned { get; set; }
    }
}
