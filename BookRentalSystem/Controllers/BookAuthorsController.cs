
using BookRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.DTO;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookAuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthor>>> GetAll()
        {
            if (!_unitOfWork.BookAuthorRepository.IfTableExists())
            {
                return NotFound();
            }

            return Ok(await _unitOfWork.BookAuthorRepository.GetAll());
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthor>> GetBookAuthorInfo(int id)
        {
            if (!_unitOfWork.BookAuthorRepository.IfTableExists())
            {
                return NotFound();
            }

            if (!await _unitOfWork.BookAuthorRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            return Ok(await _unitOfWork.BookAuthorRepository.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<BookAuthor>> AddBookAuthor([FromBody] BookAuthorDTO bookAuthorDTO)
        {
            if (!_unitOfWork.BookAuthorRepository.IfTableExists())
            {
                return NotFound();
            }

            var bAuthor = await _unitOfWork.BookAuthorRepository.AddBookAuthor(bookAuthorDTO);

            return CreatedAtAction("GetBookAuthorInfo", new { id = bAuthor.BookAuthorID }, bookAuthorDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAuthor(int id, [FromBody] BookAuthorDTO bookAuthorDTO)
        {
            if (!_unitOfWork.BookAuthorRepository.IfTableExists())
            {
                return NotFound();
            }

            if (!await _unitOfWork.BookAuthorRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _unitOfWork.BookAuthorRepository.UpdateBookAuthor(id, bookAuthorDTO);

            return Ok($"Updated Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_unitOfWork.BookAuthorRepository.IfTableExists())
            {
                return NotFound();
            }

            if (!await _unitOfWork.BookAuthorRepository.IfExists(id))
            {
                return NotFound("No record exists with that ID.");
            }

            await _unitOfWork.BookAuthorRepository.RemoveItem(id);

            return Ok($"Deleted Successfully. \nStatus Code: {StatusCodes.Status204NoContent}-No Content");
        }
    }
}

/*var result = (from book in _context.Books
                          join bookauthor in _context.BookAuthors
                          on book.BookID equals bookauthor.BookID
                          join author in _context.Authors
                          on bookauthor.AuthorID equals author.AuthorID
                          select new { book, bookauthor, author }).ToList();

            BookAuthor bookAuthor = new BookAuthor();
            foreach (var a in result)
            {
                if (a.bookauthor.BookAuthorID == id)
                {
                    a.bookauthor.Author = a.author;
                    a.bookauthor.Book = a.book;
                    /*we needed to do this step because 
                    by default LINQ does lazy loading, so data from any reference table 
                    is not fetched immediately when we run the query.
                    that's why we explicitly assign data of Book into BookAuthor.
 we could have achieved this same thing with EF ToListAsync and Include 
 methods as well and then matched the id and return the matched Bookuthor
bookAuthor = a.bookauthor;
break;
                }

            }*/