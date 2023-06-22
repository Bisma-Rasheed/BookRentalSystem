using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Services.IServices
{
    public interface IRentalsService : IService<Rental>
    {
        Task<Rental> AddRental(RentalDTO rentalDTO);
        Task UpdateRental(int id, RentalDTO rentalDTO);
    }
}
