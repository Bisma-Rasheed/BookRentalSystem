
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAll()
        {
            if (!_unitOfWork.RentalRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            return Ok(await _unitOfWork.RentalRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetById(int id)
        {
            if(!_unitOfWork.RentalRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if(!await _unitOfWork.RentalRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _unitOfWork.RentalRepository.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<RentalDTO>> AddRental([FromBody] RentalDTO rentalDTO)
        {
            if (!_unitOfWork.RentalRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            var rental = await _unitOfWork.RentalRepository.AddRentalInfo(rentalDTO);

            return CreatedAtAction("GetById", new { id = rental.RentalID }, rentalDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, [FromBody] RentalDTO rentalDTO)
        {
            if (!_unitOfWork.RentalRepository.IfTableExists())
            {
                //return StatusCode(500);
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.RentalRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
                //throw new FileNotFoundException("No record exists with that ID bisma.");
            }

            await _unitOfWork.RentalRepository.UpdateRentalInfo(id, rentalDTO);

            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (!_unitOfWork.RentalRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.RentalRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }
            
            await _unitOfWork.RentalRepository.RemoveItem(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }
    }
}
