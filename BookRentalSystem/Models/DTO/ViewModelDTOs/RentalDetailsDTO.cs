namespace BookRentalSystem.Models.DTO.ViewModelDTOs
{
    public class RentalDetailsDTO
    {
        public int RentalID { get; set; }
        public string? BookName { get; set; }
        public string? About_Book { get; set; }
        public string? Author { get; set; }
        public string? About_Author { get; set; }
        public decimal Rental_Price { get; set; }
        public decimal LateFee { get; set; }
        public decimal TotalFeeIfLate { get; set; }
    }
}
