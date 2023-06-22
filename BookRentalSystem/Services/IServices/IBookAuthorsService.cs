using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Services.IServices
{
    public interface IBookAuthorsService : IService<BookAuthor>
    {
        Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO);
        Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO);
    }
}
