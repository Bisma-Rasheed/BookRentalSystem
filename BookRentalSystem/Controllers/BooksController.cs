
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Models.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Models.DTO.ViewModelDTOs;
using Microsoft.AspNetCore.Cors;

namespace BookRentalSystem.Controllers
{
    //[Authorize(Roles = "Admin")]
    [EnableCors]
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
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            
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

        [HttpGet]
        [Route("/AvailableBooks")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> AvailableBooks()
        {
            
            try
            {
                return Ok(await _service.GetAvailableBooks());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBookInfo(BookInfoDTO bookInfoDTO)
        {
            try
            {
                var book = await _service.AddBook(bookInfoDTO);
                return CreatedAtAction("GetBook", new {id = book.BookID }, bookInfoDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Book failed to create. {ex.Message}" });
            }
            
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
           
            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateBook(id, bookUpdateDTO);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "Updated Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Book failed to update. {ex.Message}" });
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
           
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
