
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

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
                return Problem("Internal Server Error.");
            }

            return Ok(await _service.GetAllItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetById(int id)
        {
            if(!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if(!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _service.GetItem(id));
        }

        [HttpPost]
        public async Task<ActionResult<RentalDTO>> AddRental([FromBody] RentalDTO rentalDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            var rental = await _service.AddRental(rentalDTO);

            return CreatedAtAction("GetById", new { id = rental.RentalID }, rentalDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, [FromBody] RentalDTO rentalDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _service.UpdateRental(id, rentalDTO);

            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }
            
            await _service.Delete(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }
    }
}
