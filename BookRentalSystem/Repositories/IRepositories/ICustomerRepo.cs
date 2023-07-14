using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Repositories.IRepositories
{
    public interface ICustomerRepo : IGenericRepo<Customer>
    {
        Task<Customer> AddCustomer(CustomerDTO customerDTO);
        Task UpdateCustomer(string id, CustomerDTO customerDTO);
    }
}
