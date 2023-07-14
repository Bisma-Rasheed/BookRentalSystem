using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models.DTO.ViewModelDTOs
{
    public class RentalInfoAdmin
    {
        public int RentalID { get; set; }
        public int BookID { get; set; }
        public string? CustomerID { get; set; }
        public string? BookName { get; set; }
        public string? About_Book { get; set; }
        public string? isAvailable { get; set; }
        public decimal RentalPrice { get; set; }
        public int Quantity { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? Contact { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        public decimal LateFee { get; set; }
    }
}
