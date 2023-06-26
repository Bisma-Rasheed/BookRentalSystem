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

            try
            {
                return Ok(await _service.GetItem(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            

        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            try
            {
                var author = await _service.AddAuthor(authorDTO);

                return CreatedAtAction("GetAuthor", new { id = author.AuthorID }, authorDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
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

            try
            {
                await _service.UpdateAuthor(id, authorDTO);

                return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
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
                return NotFound("no record exists with that ID.");
            }

            try
            {
                await _service.Delete(id);

                return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
            }
            catch( Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }
    }
}
