using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

       public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (!_unitOfWork.AuthorRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            return Ok(await _unitOfWork.AuthorRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            if (!_unitOfWork.AuthorRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.AuthorRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _unitOfWork.AuthorRepository.GetById(id));

        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!_unitOfWork.AuthorRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            var author = await _unitOfWork.AuthorRepository.AddAuthor(authorDTO);

            return CreatedAtAction("GetAuthor", new { id = author.AuthorID }, authorDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (!_unitOfWork.AuthorRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.AuthorRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _unitOfWork.AuthorRepository.UpdateAuthor(id, authorDTO);
           
            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!_unitOfWork.AuthorRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.AuthorRepository.IfExists(id))
            {
                return NotFound("no record exists with that ID.");
            }

            await _unitOfWork.AuthorRepository.RemoveItem(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }
    }
}
