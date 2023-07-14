using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models.DTO.ModelDTOs
{
    public class AuthorDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
