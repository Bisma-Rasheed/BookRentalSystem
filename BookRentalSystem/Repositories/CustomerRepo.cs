using BookRentalSystem.Data;
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;

namespace BookRentalSystem.Repositories
{
    public class CustomerRepo : GenericRepo<Customer>,ICustomerRepo
    {
        public CustomerRepo(BRSContext context) : base(context) { }

        public async Task<Customer> AddCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new()
            {
                CustomerName = customerDTO.CustomerName,
                Contact = customerDTO.Contact
            };
            _dbSet.Add(customer);
            await Save();
            return customer;
        }

        public async Task UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            var customer = await GetById(id);
            customer.CustomerName = customerDTO.CustomerName;
            customer.Contact = customerDTO.Contact;

            UpdateDB(customer);
            Save();
            return;
        }
    }
}
