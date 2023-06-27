
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Models.ErrorHandling;

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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Book\" is null." });
            }

            try
            {
                return Ok(await _service.GetAllItems());
            }
            catch (Exception ex) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status="Error", Message=ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Book\" is null." });
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
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook([FromBody] BookDTO bookDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Book\" is null." });
            }

            try
            {
                var book = await _service.AddBook(bookDTO);

                return CreatedAtAction("GetBook", new { id = book.BookID }, bookDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Book failed to create. {ex.Message}" });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Book\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateBook(id, bookDTO);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "Updated Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Book failed to update. {ex.Message}" });
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Book\" is null." });
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
            catch( Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }

    }
}
