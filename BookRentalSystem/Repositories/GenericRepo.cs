using BookRentalSystem.Data;
using BookRentalSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookRentalSystem.Repositories
{
    public abstract class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly BRSContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepo(BRSContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task RemoveItem(int id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
            await Save();
            return;
        }

        public async Task Save()
        {
             await _context.SaveChangesAsync();
        }

        public void UpdateDB(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return;
        }
        public async Task<bool> IfExists(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }

        public bool IfTableExists()
        {
            if (_dbSet != null)
            {
                return true;
            }

            return false;
        }
    }
}
