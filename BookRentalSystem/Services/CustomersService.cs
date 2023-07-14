using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;
using NuGet.Protocol.Core.Types;

namespace BookRentalSystem.Services
{
    public class CustomersService : Service<Customer>, ICustomersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersService(IUnitOfWork unitOfWork, ICustomerRepo _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllItems(string id)
        {
            var users = await _unitOfWork.CustomerRepository.GetAll();
            List<Customer> customers = new();
            foreach(var user in users)
            {
                if (user.Id != id)
                {
                    customers.Add(user);
                }
            }
            return customers;
        }
        public async Task<Customer> AddCustomer(CustomerDTO customerDTO)
        {
            var result = await _unitOfWork.CustomerRepository.AddCustomer(customerDTO);
            return result;
        }
        public async Task UpdateCustomer(string id, CustomerDTO customerDTO)
        {
            await _unitOfWork.CustomerRepository.UpdateCustomer(id, customerDTO);
            return;
        }

        public async Task<bool> IfExists(string id)
        {
            return await _unitOfWork.CustomerRepository.IfExists(id);
        }

        public async Task Delete(string id)
        {
            await _unitOfWork.CustomerRepository.RemoveItem(id);
            return;
        }
    }
}
