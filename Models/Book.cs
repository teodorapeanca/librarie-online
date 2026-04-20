using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarieOnline.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // Foreign Keys
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }

        // Navigation Properties
        public Author? Author { get; set; }
        public Category? Category { get; set; }
        public Publisher? Publisher { get; set; }
    }
}