using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ViewModelDTOs;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Services.IServices
{
    public interface IBooksService : IService<Book>
    {
        new Task<IEnumerable<BookDTO>> GetAllItems();
        Task<Book> AddBook(BookInfoDTO bookInfoDTO);
        Task UpdateBook(int id, BookUpdateDTO bookUpdateDTO);
        Task<Book> GetBookByName(string name);
        Task<IEnumerable<BookDTO>> GetAvailableBooks();
    }

}
