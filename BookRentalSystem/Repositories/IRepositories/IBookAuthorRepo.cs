using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IBookAuthorRepo : IGenericRepo<BookAuthor>
    {
        Task<IEnumerable<BookAuthor>> GetAll();
        Task<BookAuthor> GetById(int id);
        Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO);
        Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO);
    }
}
