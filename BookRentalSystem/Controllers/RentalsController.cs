using BookRentalSystem.Models;
using BookRentalSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Models.DTO.ViewModelDTOs;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _service;

        public RentalsController(IRentalsService service)
        {
            _service = service; 
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAll()
        {
           
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
            }
            try
            {
                var rentals = await _service.GetAllItems();
                List<RentalInfoAdmin> rentalsInfo = new();
                foreach(var rental in rentals)
                {
                    rentalsInfo.Add(new RentalInfoAdmin
                    {
                        RentalID = rental.RentalID,
                        CustomerID = rental.CustomerID,
                        BookID = rental.BookID,
                        BookName = rental.Book!.Title,
                        About_Book = rental.Book.Description,
                        isAvailable = rental.Book.isAvailable,
                        RentalPrice = rental.Book.RentalPrice,
                        Quantity = rental.Book.Quantity,
                        RentalDate = rental.RentalDate,
                        ReturnDate=rental.ReturnDate,
                        LateFee=rental.LateFee,
                        CustomerName = rental.Customer!.UserName,
                        CustomerEmail = rental.Customer!.Email,
                        Contact = rental.Customer.PhoneNumber
                    });
                }
                return Ok(rentalsInfo);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetById(int id)
        {
            
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }
            try
            {
                var rental = await _service.GetItem(id);
                var rentalInfo = new RentalInfoAdmin
                {
                    RentalID = rental.RentalID,
                    CustomerID = rental.CustomerID,
                    BookID = rental.BookID,
                    BookName = rental.Book!.Title,
                    About_Book = rental.Book.Description,
                    isAvailable = rental.Book.isAvailable,
                    RentalPrice = rental.Book.RentalPrice,
                    Quantity = rental.Book.Quantity,
                    RentalDate = rental.RentalDate,
                    ReturnDate = rental.ReturnDate,
                    LateFee = rental.LateFee,
                    CustomerName = rental.Customer!.UserName,
                    CustomerEmail = rental.Customer!.Email,
                    Contact = rental.Customer.PhoneNumber
                };
                return Ok(rentalInfo);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
           
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, [FromBody] RentalDTO rentalDTO)
        {
            
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
            }
            
            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateRental(id, rentalDTO);
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

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Rental\" is null." });
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
