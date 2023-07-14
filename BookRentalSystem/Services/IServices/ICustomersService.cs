using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Services.IServices
{
    public interface ICustomersService : IService<Customer>
    {
        Task<IEnumerable<Customer>> GetAllItems(string id);
        Task<Customer> AddCustomer(CustomerDTO customerDTO);
        Task UpdateCustomer(string id, CustomerDTO customerDTO);
        //new Task<IEnumerable<Customer>> GetAllItems();
        Task<Customer> GetItem(string id);
        Task Delete(string id);
        Task<bool> IfExists(string id);
    }
}
