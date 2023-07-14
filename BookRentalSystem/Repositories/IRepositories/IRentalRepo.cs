using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IRentalRepo : IGenericRepo<Rental>
    {
        new Task<IEnumerable<Rental>> GetAll();
        new Task<Rental> GetById(int id);

        Task<IEnumerable<Rental>> GetRentalsByCustomer(string id);
        Task<Rental> AddRentalInfo(RentalDTO rentalDTO);
        Task UpdateRentalInfo(int id, RentalDTO rentalDTO);
    }
}
