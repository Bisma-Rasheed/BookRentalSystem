using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO;

namespace BookRentalSystem.Services.IServices
{
    public interface IRentBookService
    {
        Task<IEnumerable<BookAuthor>> GetBooksByAuthor(string name);
        Task<Rental> AddBookRentingInfo(RentBookDTO rentDTO, string customerID);
        Task<IEnumerable<Rental?>> GetRentalHistory(string id);
    }
}
