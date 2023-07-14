using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models.DTO.ViewModelDTOs
{
    public class BookInfoDTO
    {
        [Required, StringLength(50)]
        public string? BookName { get; set; }
        [Required]
        public string? About_Book { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? About_Author { get; set; }
        [Required, StringLength(10)]
        public string isAvailable { get; set; }
        [Required] //this required KW is for the model, i.e. it will mark the field with * denoting it is required
        public decimal RentalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
