using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class AuthorsService : Service<Author>, IAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsService(IUnitOfWork unitOfWork, IAuthorRepo _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> AddAuthor(AuthorDTO authorDTO)
        {
            var author = await _unitOfWork.AuthorRepository.GetByName(authorDTO.Name);
            if (author != null)
            {
                return author;
            }
            var result = await _unitOfWork.AuthorRepository.AddAuthor(authorDTO);
            return result;
        }

        public async Task<Author> GetByName(string name)
        {
            return await _unitOfWork.AuthorRepository.GetByName(name);
        }

        public async Task UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            await _unitOfWork.AuthorRepository.UpdateAuthor(id, authorDTO);
            return;
        }

       
    }
}
