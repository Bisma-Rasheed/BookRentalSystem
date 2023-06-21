using BookRentalSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.DTO
{
    public class BookAuthorDTO
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public int AuthorID { get; set; }
    }
}
