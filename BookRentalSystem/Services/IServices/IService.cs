namespace BookRentalSystem.Services.IServices
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllItems();
        Task<T> GetItem(int id);
        Task Delete(int id);
        Task<bool> IfExists(int id);
        bool IfTableExists();
    }
}
