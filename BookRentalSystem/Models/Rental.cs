using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }
        [Required]
        public int BookID { get; set; }
        public Book? Book { get; set; }
        [Required]
        public string? CustomerID { get; set; }
        public Customer? Customer { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        [Required]
        public decimal LateFee { get; set; }
    }
}
