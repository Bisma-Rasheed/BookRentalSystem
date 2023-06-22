using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Services.IServices
{
    public interface ICustomersService : IService<Customer>
    {
        Task<Customer> AddCustomer(CustomerDTO customerDTO);
        Task UpdateCustomer(int id, CustomerDTO customerDTO);
    }
}
