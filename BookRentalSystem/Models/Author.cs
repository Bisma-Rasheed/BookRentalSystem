using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
