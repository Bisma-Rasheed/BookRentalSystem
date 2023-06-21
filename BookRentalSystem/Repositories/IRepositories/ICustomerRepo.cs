using BookRentalSystem.DTO;
using BookRentalSystem.Models;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface ICustomerRepo : IGenericRepo<Customer>
    {
        Task<Customer> AddCustomer(CustomerDTO customerDTO);
        Task UpdateCustomer(int id, CustomerDTO customerDTO);
    }
}
