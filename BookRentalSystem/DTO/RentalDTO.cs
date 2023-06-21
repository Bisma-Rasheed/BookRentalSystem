using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.DTO
{
    public class RentalDTO
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public int CustomerID { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        [Required]
        public decimal LateFee { get; set; }
    }
}
