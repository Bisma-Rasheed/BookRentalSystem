using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IAuthorRepo : IGenericRepo<Author>
    {
        Task<Author> GetByName(string name);
        Task<Author> AddAuthor(AuthorDTO authorDTO);
        Task UpdateAuthor(int id, AuthorDTO authorDTO);
    }
}
