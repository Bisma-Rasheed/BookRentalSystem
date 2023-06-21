using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IRentalRepo : IGenericRepo<Rental>
    {
        new Task<IEnumerable<Rental>> GetAll();
        new Task<Rental> GetById(int id);
        Task<Rental> AddRentalInfo(RentalDTO rentalDTO);
        Task UpdateRentalInfo(int id, RentalDTO rentalDTO);
    }
}
