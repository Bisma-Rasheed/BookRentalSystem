

using BookRentalSystem.Data;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task RemoveItem(int id);
        Task RemoveItem(string id);
        Task<bool> IfExists(int id);
        Task<bool> IfExists(string id);
        Task Save();
        void UpdateDB(T entity);
        bool IfTableExists();
        
    }
}
