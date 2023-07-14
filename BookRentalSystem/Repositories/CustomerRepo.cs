using BookRentalSystem.Data;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Repositories.IRepositories;

namespace BookRentalSystem.Repositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(BRSContext context) : base(context) { }

        public async Task<Customer> AddCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new()
            {
                UserName = customerDTO.CustomerName,
                PhoneNumber = customerDTO.Contact
            };
            _dbSet.Add(customer);
            await Save();
            return customer;
        }

        public async Task UpdateCustomer(string id, CustomerDTO customerDTO)
        {
            var customer = await GetById(id);
            customer.UserName = customerDTO.CustomerName;
            customer.PhoneNumber = customerDTO.Contact;

            UpdateDB(customer);
            await Save();
            return;
        }
    }
}
