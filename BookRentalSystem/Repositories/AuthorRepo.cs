using BookRentalSystem.Data;
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using BookRentalSystem.Repositories.IRepositories;

namespace BookRentalSystem.Repositories
{
    public class AuthorRepo : GenericRepo<Author>, IAuthorRepo
    {
        public AuthorRepo(BRSContext context) : base(context) { }

        public async Task<Author> AddAuthor(AuthorDTO authorDTO)
        {
            Author author = new()
            {
                Name = authorDTO.Name,
                Biography = authorDTO.Biography,
            };
            _dbSet.Add(author);
            await Save();
            return author;
        }

        public async Task UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            var author = await GetById(id);
            author.Name = authorDTO.Name;
            author.Biography = authorDTO.Biography;

            UpdateDB(author);
            await Save();
            return;
        }
    }
}
