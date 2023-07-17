using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Models.DTO.ViewModelDTOs;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class BooksService : Service<Book>, IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorsService _authorsService;
        private readonly IBookAuthorsService _bookAuthorsService;

        public BooksService(IUnitOfWork unitOfWork, IBookRepo _repo, IAuthorsService authorsService, IBookAuthorsService bookAuthorsService) : base(_repo)
        {
            _unitOfWork = unitOfWork;
            _authorsService = authorsService;
            _bookAuthorsService = bookAuthorsService;
        }

        public new async Task<IEnumerable<BookDTO>> GetAllItems()
        {
            var books = await _unitOfWork.BookRepository.GetAll();
            List<BookDTO> bookDTO = new();
            foreach(var book in books)
            {
                bookDTO.Add(new BookDTO
                {
                    Title = book.Title!,
                    Description = book.Description!,
                    isAvailable = book.isAvailable!,
                    RentalPrice = book.RentalPrice,
                    Quantity = book.Quantity
                });
            }
            return bookDTO;
        }
        public async Task<Book> AddBook(BookInfoDTO bookInfoDTO)
        {
            if (bookInfoDTO.Quantity <= 0) bookInfoDTO.isAvailable = "No";
            else bookInfoDTO.isAvailable = "Yes";

            AuthorDTO authorDTO = new()
            {
                Name = bookInfoDTO.Author!,
                Biography = bookInfoDTO.About_Author!
            };
            try
            {
                var author = await _authorsService.AddAuthor(authorDTO);
                var book = await _unitOfWork.BookRepository.GetByName(bookInfoDTO.BookName!);

                if (book == null)
                {
                    BookDTO bookDTO = new()
                    {
                        Title = bookInfoDTO.BookName!,
                        Description = bookInfoDTO.About_Book!,
                        isAvailable = bookInfoDTO.isAvailable,
                        RentalPrice = bookInfoDTO.RentalPrice,
                        Quantity = bookInfoDTO.Quantity
                    };
                    try
                    {
                        book = await _unitOfWork.BookRepository.AddBook(bookDTO);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }

                BookAuthorDTO bAuthor = new()
                {
                    AuthorID = author.AuthorID,
                    BookID = book.BookID
                };
                try
                {
                    await _bookAuthorsService.AddBookAuthor(bAuthor);

                    await _unitOfWork.Save();
                    return book;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> GetBookByName(string name)
        {
            return await _unitOfWork.BookRepository.GetByName(name);
        }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return await _unitOfWork.BookRepository.GetAvailableBooks();
        }

        public async Task UpdateBook(int id, BookUpdateDTO bookUpdateDTO)
        {
            string isAvailable;
            if (bookUpdateDTO.Quantity <= 0) isAvailable = "No";
            else isAvailable = "Yes";
            var bookDTO = new BookDTO
            {
                isAvailable = isAvailable,
                RentalPrice = bookUpdateDTO.RentalPrice,
                Quantity = bookUpdateDTO.Quantity
            };
            await _unitOfWork.BookRepository.UpdateBook(id, bookDTO);
            return;
        }
    }
}
