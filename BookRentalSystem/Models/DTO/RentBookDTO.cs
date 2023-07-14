using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models.DTO
{
    public class RentBookDTO
    {
        [Required]
        public string BookName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
