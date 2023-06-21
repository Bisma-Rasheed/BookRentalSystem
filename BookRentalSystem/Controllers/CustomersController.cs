
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if(!_unitOfWork.CustomerRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            return Ok(await _unitOfWork.CustomerRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (!_unitOfWork.CustomerRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (! await _unitOfWork.CustomerRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _unitOfWork.CustomerRepository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (!_unitOfWork.CustomerRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.CustomerRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _unitOfWork.CustomerRepository.UpdateCustomer(id, customerDTO);

            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");

            
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            if (!_unitOfWork.CustomerRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            var customer = await _unitOfWork.CustomerRepository.AddCustomer(customerDTO);


            return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customerDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!_unitOfWork.CustomerRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.CustomerRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }                                                                       

            await _unitOfWork.CustomerRepository.RemoveItem(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

    }
}
