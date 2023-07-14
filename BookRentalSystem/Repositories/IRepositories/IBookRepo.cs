using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IBookRepo : IGenericRepo<Book>
    {
        Task<Book> AddBook(BookDTO bookDTO);
        Task UpdateBook(int id, BookDTO bookDTO);
        Task<Book> GetByName(string name);
        Task<IEnumerable<Book>> GetAvailableBooks();
    }
}
