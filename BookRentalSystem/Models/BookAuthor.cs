using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRentalSystem.Models
{
    public class BookAuthor
    {
        [Key]
        public int BookAuthorID { get; set; }
        [Required]
        public int BookID { get; set; }
        public Book? Book { get; set; }
        [Required]
        public int AuthorID { get; set; }
        public Author? Author { get; set; }
    }
}
