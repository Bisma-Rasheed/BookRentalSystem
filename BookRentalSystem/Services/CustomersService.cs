using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class CustomersService : Service<Customer>, ICustomersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersService(IUnitOfWork unitOfWork, IGenericRepo<Customer> _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> AddCustomer(CustomerDTO customerDTO)
        {
            var result = await _unitOfWork.CustomerRepository.AddCustomer(customerDTO);
            return result;
        }
        public async Task UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            await _unitOfWork.CustomerRepository.UpdateCustomer(id, customerDTO);
            return;
        }

        
    }
}
