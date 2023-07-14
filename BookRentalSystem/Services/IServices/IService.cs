namespace BookRentalSystem.Services.IServices
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllItems();
        Task<T> GetItem(int id);
        //Task<T> GetItem(string id);
        Task Delete(int id);
        Task<bool> IfExists(int id);
        //Task<bool> IfExists(string id);
        bool IfTableExists();
    }
}
