using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Services.IServices
{
    public interface IBookAuthorsService : IService<BookAuthor>
    {
        Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO);
        Task<BookAuthor> GetByBookId(int bookID);
        Task<IEnumerable<BookAuthor>> GetByAuthorId(int id);
        Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO);
    }
}
