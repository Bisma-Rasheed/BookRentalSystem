
using BookRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Models.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using BookRentalSystem.Models.DTO.ModelDTOs;

namespace BookRentalSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorsService _service;

        public BookAuthorsController(IBookAuthorsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthor>>> GetAll()
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"BookAuthors\" is null." });
            }
            try
            {
                return Ok(await _service.GetAllItems());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthor>> GetBookAuthorInfo(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"BookAuthors\" is null." });
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }

        //[HttpPost]
        //public async Task<ActionResult<BookAuthor>> AddBookAuthor([FromBody] BookAuthorDTO bookAuthorDTO)
        //{
        //    if (!_service.IfTableExists())
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new Response { Status = "Error", Message = "The entity set \"BookAuthors\" is null." });
        //    }

        //    try
        //    {
        //        var bAuthor = await _service.AddBookAuthor(bookAuthorDTO);
        //        return CreatedAtAction("GetBookAuthorInfo", new { id = bAuthor.BookAuthorID }, bookAuthorDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new Response
        //            {
        //                Status = "Error",
        //                Message = $"DBUpdateException: {ex.Message} " +
        //            $"The INSERT statement conflicted with the FOREIGN KEY constraint."
        //            });
        //    }


        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAuthor(int id, [FromBody] BookAuthorDTO bookAuthorDTO)
        {
            
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"BookAuthors\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateBookAuthor(id, bookAuthorDTO);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "Updated Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = $"DBUpdateException: {ex.Message} " +
                    $"The UPDATE statement conflicted with the FOREIGN KEY constraint."
                    });
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"BookAuthors\" is null." });
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
           
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