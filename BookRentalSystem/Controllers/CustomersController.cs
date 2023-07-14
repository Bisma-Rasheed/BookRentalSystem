
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Models.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Models.DTO.ViewModelDTOs;

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

        [Authorize(Roles ="Admin")]
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customers = await _service.GetAllItems(userId!);
                List<CustomerInfoDTO> customersInfo = new();
                foreach (var customer in customers)
                {
                    customersInfo.Add(new CustomerInfoDTO 
                    { 
                        Id = customer.Id,
                        Username = customer.UserName,
                        Email = customer.Email,
                        EmailConfirmed = customer.EmailConfirmed,
                        PhoneNumber = customer.PhoneNumber,
                        PhoneNumberConfirmed = customer.PhoneNumberConfirmed,
                        TwoFactorEnabled = customer.TwoFactorEnabled,
                    });
                }
                return Ok(customersInfo);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Customer\" is null." });
            }

            if (!(User.FindFirstValue(ClaimTypes.Role) == "Admin"))
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            }
            //return Ok();
            if (!await _service.IfExists(id!))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }
            try
            {
                var customer = await _service.GetItem(id);
                var customerInfo = new CustomerInfoDTO
                {
                    Id = customer.Id,
                    Username = customer.UserName,
                    Email = customer.Email,
                    EmailConfirmed = customer.EmailConfirmed,
                    PhoneNumber = customer.PhoneNumber,
                    PhoneNumberConfirmed = customer.PhoneNumberConfirmed,
                    TwoFactorEnabled = customer.TwoFactorEnabled,
                };
                return Ok(customerInfo);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(string id, CustomerDTO customerDTO)
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

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
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
