

using BookRentalSystem.Data;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task RemoveItem(int id);
        Task<bool> IfExists(int id);
        Task Save();
        void UpdateDB(T entity);
        bool IfTableExists();
    }
}
