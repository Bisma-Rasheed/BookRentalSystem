using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models.ErrorHandling;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _service;

        public RentalsController(IRentalsService service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAll()
        {
           
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
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
        public async Task<ActionResult<Rental>> GetById(int id)
        {
            
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
            }

            if (!await _service.IfExists(id))
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

        [HttpPost]
        public async Task<ActionResult<RentalDTO>> AddRental([FromBody] RentalDTO rentalDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
            }

            try
            {
                var rental = await _service.AddRental(rentalDTO);

                return CreatedAtAction("GetById", new { id = rental.RentalID }, rentalDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = $"DBUpdateException: {ex.Message} " +
                    $"The INSERT statement conflicted with the FOREIGN KEY constraint."
                    });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, [FromBody] RentalDTO rentalDTO)
        {
            
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
            }
            
            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateRental(id, rentalDTO);
                return StatusCode(StatusCodes.Status200OK,
                   new Response { Status = "Success", Message = "Updated Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = $"DBUpdateException: {ex.Message} " +
                    $"The UPDATE statement conflicted with the FOREIGN KEY constraint."
                    });
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
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
