using BookRentalSystem.Data;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
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
            return bookAuthor!;
        }
        public async Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO)
        {
            try
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
            catch(Exception) { throw; } 
        }

        public async Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO)
        {
            try
            {
                var bookAuthor = await GetById(id);
                bookAuthor.BookID = bookAuthorDTO.BookID;
                bookAuthor.AuthorID = bookAuthorDTO.AuthorID;

                UpdateDB(bookAuthor);
                await Save();
                return;
            }
            catch (Exception) { throw; }
            
        }

        public async Task<BookAuthor> GetByBookId(int bookID)
        {
            var bAuthor =  await _dbSet.Where(ba => ba.BookID == bookID).SingleOrDefaultAsync();
            return bAuthor!;
        }

        public async Task<IEnumerable<BookAuthor>> GetByAuthorId(int id)
        {
            var bAuthor = await _dbSet.Where(ba => ba.AuthorID == id).Include(ba => ba.Book)
                .Include(ba=>ba.Author).ToListAsync();
            return bAuthor;
        }
    }
}
