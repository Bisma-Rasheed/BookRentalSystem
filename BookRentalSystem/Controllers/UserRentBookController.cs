using BookRentalSystem.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models.ErrorHandling;
using System.Security.Claims;
using BookRentalSystem.Models;
using System.Data;
using BookRentalSystem.Models.DTO.ViewModelDTOs;
using BookRentalSystem.Models.DTO;

namespace BookRentalSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserRentBookController : ControllerBase
    {
        private readonly IRentBookService _service;
        private readonly IBookAuthorsService _baService;
        private readonly IAuthorsService _authorService;

        public UserRentBookController(IRentBookService service, IBookAuthorsService baService, IAuthorsService authorsService)
        {
            _service = service;
            _baService = baService;
            _authorService = authorsService;

        }


        [HttpGet]
        [Route("/author/books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(string authorName)
        {
            var booksAuthor = await _service.GetBooksByAuthor(authorName);
            if (booksAuthor != null)
            {
                List<BookInfoDTO> booksInfo = new();
                foreach (var b in booksAuthor)
                {
                    booksInfo.Add(new BookInfoDTO
                    {
                        BookName = b.Book!.Title,
                        About_Book = b.Book!.Description,
                        Author = b.Author!.Name,
                        About_Author = b.Author!.Biography,
                        RentalPrice = b.Book.RentalPrice,
                        isAvailable = b.Book.isAvailable!,
                        Quantity = b.Book.Quantity

                    });
                }
                return Ok(booksInfo);
            }
            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Not Found", 
                    Message = "Sorry! Currently we don't have any books of given author.", Reason = "" });
            
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("/details")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistory()
        {
            try
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var rentals = await _service.GetRentalHistory(userID!);
                List<BookAuthor> bAuthors = new();
                foreach (var rental in rentals)
                {
                    bAuthors.Add(await _baService.GetByBookId(rental!.BookID));
                }
                List<Author> authors = new();
                foreach(var ba in bAuthors)
                {
                    authors.Add(await _authorService.GetItem(ba.AuthorID));
                }
                List<RentalDetailsDTO> userRentals = new();
                using (var r = rentals.GetEnumerator())
                using(var a = authors.GetEnumerator())
                {
                    while(r.MoveNext() && a.MoveNext())
                    {
                        userRentals.Add(new RentalDetailsDTO
                        {
                            RentalID = r.Current!.RentalID,
                            BookName = r.Current!.Book!.Title,
                            About_Book = r.Current!.Book.Description,
                            Author = a.Current.Name,
                            About_Author = a.Current.Biography,
                            Rental_Price = r.Current.Book.RentalPrice,
                            LateFee = r.Current.LateFee,
                            TotalFeeIfLate = r.Current.Book.RentalPrice+r.Current.LateFee

                        });
                    }
                }
                return Ok(new
                {
                    Rented_Books = userRentals,
                    StatusCode = 200
                });
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = $"{ex.Message}", Reason = "" });
            }
            
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("/rentbook")]
        public async Task<IActionResult> AddBookRentingInfo(RentBookDTO rentDTO)
        {
            try
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var rental = await _service.AddBookRentingInfo(rentDTO, userID!);
                if (rental == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        new Response
                        {
                            Status = "Error",
                            Message = "Can not rent book.",
                            Reason = $"Oops! Seems like we ran out of {rentDTO.BookName}."
                        });
                }
                return Ok(new Response
                {
                    Status="Success",
                    Message = $"{rentDTO.BookName} has been successfully rented.",
                    Reason = "-"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "DBUpdateException",
                        Reason = $"{ex.Message} "
                    });
            }
        }
    }
}
