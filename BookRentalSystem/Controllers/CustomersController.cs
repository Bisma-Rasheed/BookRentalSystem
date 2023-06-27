
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Models.ErrorHandling;

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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Customer\" is null." });
            }
            try
            {
                return Ok(await _service.GetAllItems());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Customer\" is null." });
            }

            if (! await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }
            try
            {
                return Ok(await _service.GetItem(id));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Customer\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateCustomer(id, customerDTO);

                return StatusCode(StatusCodes.Status200OK,
                   new Response { Status = "Success", Message = "Updated Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Customer failed to update. {ex.Message}" });
            }
            

            
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Customer\" is null." });
            }
            try
            {
                var customer = await _service.AddCustomer(customerDTO);

                return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customerDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Customer failed to create. {ex.Message}" });
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Customer\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }
            try
            {
                await _service.Delete(id);

                return StatusCode(StatusCodes.Status200OK,
                   new Response { Status = "Success", Message = "Deleted Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
        }

    }
}
