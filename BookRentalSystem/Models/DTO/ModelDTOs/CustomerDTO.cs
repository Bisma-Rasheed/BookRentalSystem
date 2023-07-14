using BookRentalSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models.DTO.ModelDTOs
{
    public class CustomerDTO
    {
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        [Required, StringLength(15)]
        public string Contact { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
    }
}
