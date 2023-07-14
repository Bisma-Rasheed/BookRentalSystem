using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Services.IServices
{
    public interface IRentalsService : IService<Rental>
    {
        Task<Rental> AddRental(RentalDTO rentalDTO);
        Task UpdateRental(int id, RentalDTO rentalDTO);
        Task<IEnumerable<Rental>> GetRentalsByCustomer(string id);
    }
}
