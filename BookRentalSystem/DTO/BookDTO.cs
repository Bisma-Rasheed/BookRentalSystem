using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.DTO
{
    public class BookDTO
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, StringLength(10)]
        public string isAvailable { get; set; }
        [Required] //this required KW is for the model, i.e. it will mark the field with * denoting it is required
        public decimal RentalPrice { get; set; }
    }
}
