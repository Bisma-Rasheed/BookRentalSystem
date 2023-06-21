using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IAuthorRepo : IGenericRepo<Author>
    {
        Task<Author> AddAuthor(AuthorDTO authorDTO);
        Task UpdateAuthor(int id, AuthorDTO authorDTO);
    }
}
