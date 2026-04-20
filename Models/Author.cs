using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Nationality { get; set; }
    }
}