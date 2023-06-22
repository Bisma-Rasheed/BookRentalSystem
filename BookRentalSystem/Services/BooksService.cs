using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class BooksService : Service<Book>, IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork unitOfWork, IGenericRepo<Book> _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> AddBook(BookDTO bookDTO)
        {
            var result = await _unitOfWork.BookRepository.AddBook(bookDTO);
            return result;
        }

        public async Task UpdateBook(int id, BookDTO bookDTO)
        {
            await _unitOfWork.BookRepository.UpdateBook(id, bookDTO);
            return;
        }
    }
}
