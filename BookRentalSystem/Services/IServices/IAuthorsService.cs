using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Services.IServices
{
    public interface IAuthorsService : IService<Author>
    {
        Task<Author> GetByName(string name);
        Task<Author> AddAuthor(AuthorDTO authorDTO);
        Task UpdateAuthor(int id, AuthorDTO authorDTO);
    }
}
