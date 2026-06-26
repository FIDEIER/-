using System.ComponentModel.DataAnnotations;

namespace КУРСАЧ.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Librarian";
    }
}
