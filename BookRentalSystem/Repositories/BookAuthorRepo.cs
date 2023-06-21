using BookRentalSystem.Data;
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookRentalSystem.Repositories
{
    public class BookAuthorRepo : GenericRepo<BookAuthor>, IBookAuthorRepo
    {
        public BookAuthorRepo(BRSContext context) : base(context) { }

        public new async Task<IEnumerable<BookAuthor>> GetAll()
        {
            return await _dbSet.Include(ba => ba.Book).Include(ba => ba.Author).ToListAsync();
        }

        public new async Task<BookAuthor> GetById(int id)
        {
            BookAuthor? bookAuthor = await _dbSet.Where(ba => ba.BookAuthorID == id)
                .Include(ba => ba.Book).Include(ba => ba.Author).SingleOrDefaultAsync();
            return bookAuthor;
        }
        public async Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO)
        {
            BookAuthor bookAuthor = new()
            {
                BookID = bookAuthorDTO.BookID,
                AuthorID = bookAuthorDTO.AuthorID
            };

            _dbSet.Add(bookAuthor);
            await Save();
            return bookAuthor;
        }

        public async Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO)
        {
            var bookAuthor = await GetById(id);
            bookAuthor.BookID = bookAuthorDTO.BookID;
            bookAuthor.AuthorID = bookAuthorDTO.AuthorID;

            UpdateDB(bookAuthor);
            await Save();
            return;
        }
    }
}
