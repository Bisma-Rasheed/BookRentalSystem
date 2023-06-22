using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Services.IServices
{
    public interface IBooksService : IService<Book>
    {
        Task<Book> AddBook(BookDTO bookDTO);
        Task UpdateBook(int id, BookDTO bookDTO);
    }
}
