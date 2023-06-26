using BookRentalSystem.Data;
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookRentalSystem.Repositories
{
    public class BookRepo : GenericRepo<Book>, IBookRepo
    {
        public BookRepo(BRSContext context) : base(context) { }

        public async Task<Book> AddBook(BookDTO bookDTO)
        {
            Book book = new()
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                isAvailable = bookDTO.isAvailable,
                RentalPrice = bookDTO.RentalPrice
            };
            _dbSet.Add(book);
            await Save();
            return book;
        }

        public async Task UpdateBook(int id, BookDTO bookDTO)
        {
            var book = await GetById(id);
            book.Title = bookDTO.Title;
            book.Description = bookDTO.Description;
            book.isAvailable = bookDTO.isAvailable;
            book.RentalPrice = bookDTO.RentalPrice;

            UpdateDB(book);
            await Save();
            return;
        }
    }
}
 