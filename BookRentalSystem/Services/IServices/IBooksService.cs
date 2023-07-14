using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ViewModelDTOs;

namespace BookRentalSystem.Services.IServices
{
    public interface IBooksService : IService<Book>
    {
        Task<Book> AddBook(BookInfoDTO bookInfoDTO);
        Task UpdateBook(int id, BookUpdateDTO bookUpdateDTO);
        Task<Book> GetBookByName(string name);
        Task<IEnumerable<Book>> GetAvailableBooks();
    }

}
