using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IBookRepo : IGenericRepo<Book>
    {
        Task<Book> AddBook(BookDTO bookDTO);
        Task UpdateBook(int id, BookDTO bookDTO);
    }
}
