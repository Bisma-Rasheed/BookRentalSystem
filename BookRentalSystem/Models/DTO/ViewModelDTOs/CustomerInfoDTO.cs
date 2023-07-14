using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models.DTO.ViewModelDTOs
{
    public class CustomerInfoDTO
    {
        [Required]
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
