using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class RentBookService : IRentBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentalsService _rentalService;
        private readonly IBooksService _bookService;
        private readonly IAuthorsService _authorsService;
        private readonly ICustomersService _customersService;
        private readonly IBookAuthorsService _bookAuthorService;

        public RentBookService(IRentalsService service, IUnitOfWork unitOfWork, IBooksService bookService,
            ICustomersService customersService, IAuthorsService authorsService, IBookAuthorsService bookAuthorService)
        {
            _rentalService = service;
            _unitOfWork = unitOfWork;
            _bookService = bookService;
            _customersService = customersService;
            _authorsService = authorsService;
            _bookAuthorService = bookAuthorService;
        }

        public async Task<IEnumerable<BookAuthor>> GetBooksByAuthor(string authorName)
        {
            var author = await _authorsService.GetByName(authorName);
            if (author != null)
            {
                var bAuthors = await _bookAuthorService.GetByAuthorId(author.AuthorID);
                return bAuthors;
            }
            return null;

        }
        public async Task<Rental?> AddBookRentingInfo(RentBookDTO rentDTO, string customerID)
        {
            var book = await _bookService.GetBookByName(rentDTO.BookName);
            
            if (book != null && !(book.Quantity<=0))
            {
                RentalDTO rentalDTO = new()
                {
                    BookID = book.BookID,
                    CustomerID = customerID,
                    RentalDate = DateTime.Now,
                    ReturnDate = rentDTO.ReturnDate,
                    LateFee = book.RentalPrice * (decimal)0.3
                };

                var customer = await _customersService.GetItem(customerID);
                var customerDTO = new CustomerDTO
                {
                    CustomerName = customer.UserName!,
                    Contact = rentDTO.PhoneNumber,
                    Email = customer.Email!

                };

                await _customersService.UpdateCustomer(customerID, customerDTO);

                var rental = await _rentalService.AddRental(rentalDTO);
                
                var bookUpdateDTO = new BookUpdateDTO
                {
                    RentalPrice = book.RentalPrice,
                    Quantity = book.Quantity - 1
                };
                await _bookService.UpdateBook(book.BookID, bookUpdateDTO);

                await _unitOfWork.Save();

                return rental;
            }
            return null;
            
        }


        public async Task<IEnumerable<Rental>> GetRentalHistory(string id)
        {
            var rentals = await _rentalService.GetRentalsByCustomer(id);
            return rentals;
        }
    }
}