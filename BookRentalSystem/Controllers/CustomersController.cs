
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.Services.IServices;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if(!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }
            try
            {
                return Ok(await _service.GetAllItems());
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (! await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }
            try
            {
                return Ok(await _service.GetItem(id));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            try
            {
                await _service.UpdateCustomer(id, customerDTO);

                return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }
            try
            {
                var customer = await _service.AddCustomer(customerDTO);

                return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customerDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }
            try
            {
                await _service.Delete(id);

                return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
