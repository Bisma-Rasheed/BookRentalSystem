using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IBookAuthorRepo : IGenericRepo<BookAuthor>
    {
        Task<IEnumerable<BookAuthor>> GetAll();
        new Task<BookAuthor> GetById(int id);
        Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO);
        Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO);
        Task<BookAuthor> GetByBookId(int bookID);
        Task<IEnumerable<BookAuthor>> GetByAuthorId(int id);
    }
}
