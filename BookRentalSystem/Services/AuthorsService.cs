using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class AuthorsService : Service<Author>, IAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsService(IUnitOfWork unitOfWork, IGenericRepo<Author> _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }
        //public async Task<IEnumerable<Author>> GetAllAuthors()
        //{
        //    var result = await _unitOfWork.AuthorRepository.GetAll();
        //    return result;
        //}

        //public async Task<Author> GetAuthor(int id)
        //{
        //    var result = await _unitOfWork.AuthorRepository.GetById(id);
        //    return result;
        //}
        public async Task<Author> AddAuthor(AuthorDTO authorDTO)
        {
            var result = await _unitOfWork.AuthorRepository.AddAuthor(authorDTO);
            return result;
        }
        public async Task UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            await _unitOfWork.AuthorRepository.UpdateAuthor(id, authorDTO);
            return;
        }

        //public async Task DeleteAuthor(int id)
        //{
        //    await _unitOfWork.AuthorRepository.RemoveItem(id);
        //    return;
        //}

        //public async Task<bool> IfExists(int id)
        //{
        //    return await _unitOfWork.AuthorRepository.IfExists(id);
        //}

        //public bool IfTableExists()
        //{
        //    return _unitOfWork.AuthorRepository.IfTableExists();
        //}

       
    }
}
