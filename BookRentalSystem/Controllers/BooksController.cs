
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models;
using BookRentalSystem.DTO;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            if (!_unitOfWork.BookRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            return Ok(await _unitOfWork.BookRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (!_unitOfWork.BookRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.BookRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _unitOfWork.BookRepository.GetById(id));
        }


        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook([FromBody] BookDTO bookDTO)
        {
            if (!_unitOfWork.BookRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            var book = await _unitOfWork.BookRepository.AddBook(bookDTO);

            return CreatedAtAction("GetBook", new { id = book.BookID }, bookDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (!_unitOfWork.BookRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.BookRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _unitOfWork.BookRepository.UpdateBook(id, bookDTO);

            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!   _unitOfWork.BookRepository.IfTableExists())
            {
                return Problem("Internal Server Error.");
            }

            if (!await _unitOfWork.BookRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _unitOfWork.BookRepository.RemoveItem(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

    }
}
