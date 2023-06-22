using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Services.IServices
{
    public interface IAuthorsService : IService<Author>
    {
        Task<Author> AddAuthor(AuthorDTO authorDTO);
        Task UpdateAuthor(int id, AuthorDTO authorDTO);
    }
}
