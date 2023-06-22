using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;

namespace BookRentalSystem.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepo<T> _repository;

        public Service(IGenericRepo<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllItems()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetItem(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> IfExists(int id)
        {
            return await _repository.IfExists(id);
        }

        public bool IfTableExists()
        {
            return _repository.IfTableExists();
        }

        public async Task Delete(int id)
        {
            await _repository.RemoveItem(id);
            return;
        }
    }
}
