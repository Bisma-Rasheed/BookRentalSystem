using BookRentalSystem.Data;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
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
                RentalPrice = bookDTO.RentalPrice,
                Quantity = bookDTO.Quantity
            };
            _dbSet.Add(book);
            await Save();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = await _dbSet.Where(b => b.isAvailable == "Yes").ToListAsync();
            return books;
        }

        public async Task<Book> GetByName(string name)
        {
            var book = await _dbSet.Where(b=>b.Title == name).FirstOrDefaultAsync();
            return book!;
        }

        public async Task UpdateBook(int id, BookDTO bookDTO)
        {
            var book = await GetById(id);

            book.isAvailable = bookDTO.isAvailable;
            book.RentalPrice = bookDTO.RentalPrice;
            book.Quantity = bookDTO.Quantity;

            UpdateDB(book);
            await Save();
            return;
        }
    }
}
 