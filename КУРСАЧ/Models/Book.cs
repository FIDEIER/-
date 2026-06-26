using System.ComponentModel.DataAnnotations;

namespace КУРСАЧ.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(1000, 2025)]
        public int Year { get; set; }

        [Range(1, 5000)]
        public int Pages { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = "В наличии";

        public ICollection<Author> Authors { get; set; } = new List<Author>();

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
