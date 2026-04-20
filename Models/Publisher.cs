using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Country { get; set; }
    }
}
