using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class BookAuthorsService : Service<BookAuthor> ,IBookAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookAuthorsService(IUnitOfWork unitOfWork, IBookAuthorRepo _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookAuthor> AddBookAuthor(BookAuthorDTO bookAuthorDTO)
        {
            return await _unitOfWork.BookAuthorRepository.AddBookAuthor(bookAuthorDTO);
        }

        public async Task<IEnumerable<BookAuthor>> GetByAuthorId(int id)
        {
            return await _unitOfWork.BookAuthorRepository.GetByAuthorId(id);
        }
        public async Task<BookAuthor> GetByBookId(int bookID)
        {
            return await _unitOfWork.BookAuthorRepository.GetByBookId(bookID);
        }

        public async Task UpdateBookAuthor(int id, BookAuthorDTO bookAuthorDTO)
        {
            await _unitOfWork.BookAuthorRepository.UpdateBookAuthor(id, bookAuthorDTO);
            return;
        }
    }
}
