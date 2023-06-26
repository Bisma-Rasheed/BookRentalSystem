
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.Services.IServices;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;
        public BooksController(IBooksService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            try
            {
                return Ok(await _service.GetAllItems());
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
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
            catch (Exception ex) { return Problem(ex.Message); }
        }


        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook([FromBody] BookDTO bookDTO)
        {
            if (!_service.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            try
            {
                var book = await _service.AddBook(bookDTO);

                return CreatedAtAction("GetBook", new { id = book.BookID }, bookDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
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
                await _service.UpdateBook(id, bookDTO);

                return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
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
            catch( Exception ex) 
            { 
                return Problem(ex.Message);
            }
            
        }

    }
}
