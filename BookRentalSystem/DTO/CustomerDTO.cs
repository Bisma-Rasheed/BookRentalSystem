using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.DTO
{
    public class CustomerDTO
    {
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        [Required, StringLength(15)]
        public string Contact { get; set; }
    }
}
