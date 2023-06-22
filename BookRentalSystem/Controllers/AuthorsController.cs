using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _service;

       public AuthorsController(IAuthorsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            return Ok(await _service.GetAllItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _service.GetItem(id));

        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            var author = await _service.AddAuthor(authorDTO);

            return CreatedAtAction("GetAuthor", new { id = author.AuthorID }, authorDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _service.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _service.UpdateAuthor(id, authorDTO);
           
            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
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
                return NotFound("no record exists with that ID.");
            }

            await _service.Delete(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }
    }
}
