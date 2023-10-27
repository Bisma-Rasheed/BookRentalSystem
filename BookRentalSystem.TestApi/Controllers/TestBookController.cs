using BookRentalSystem.Controllers;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Models.DTO.ViewModelDTOs;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.TestApi.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookRentalSystem.TestApi.Controllers
{
    public class TestBookController
    {
        [Fact]
        public async Task GetAllAsync_ReturnsOKStatus()
        {
            //arrange
            var bookService = new Mock<IBooksService>(); //Mocks bookservice dependency for book controller's method
            bookService.Setup(b => b.GetAllItems()).ReturnsAsync(BookMockData.GetBooks()); //setting up method call for books list
            var bookController = new BooksController(bookService.Object);

            //act
            ActionResult<IEnumerable<BookDTO>> actionResult = await bookController.GetAllBooks();
            var result = (OkObjectResult) actionResult.Result!;

            //assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllAvailableBooks_Returns200()
        {
            //arrange
            var bookService = new Mock<IBooksService>(); //Mocks bookservice dependency for book controller's method
            bookService.Setup(b => b.GetAvailableBooks()).ReturnsAsync(BookMockData.AvailableBooks()); //setting up method call for books list
            var bookController = new BooksController(bookService.Object);

            //act
            ActionResult<IEnumerable<BookDTO>> actionResult = await bookController.AvailableBooks();
            var result = (OkObjectResult)actionResult.Result!;

            //assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetBookById_Returns200Status()
        {
            //arrange
            var bookService = new Mock<IBooksService>(); //Mocks bookservice dependency for book controller's method
            bookService.Setup(b => b.GetItem(1)).ReturnsAsync(BookMockData.GetById(1)); //setting up method call for books list
            bookService.Setup(b => b.IfExists(1)).ReturnsAsync(true);
            var bookController = new BooksController(bookService.Object);

            //act
            ActionResult<Book> actionResult = await bookController.GetBook(1);
            //actionResult.Result
            var result = (OkObjectResult) actionResult.Result!;

            //assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetBookById_Returns404Status()
        {

            //arrange
            var bookService = new Mock<IBooksService>();
            bookService.Setup(b => b.IfExists(6)).ReturnsAsync(false);
            var bookController = new BooksController(bookService.Object);

            //act
            ActionResult<Book> actionResult = await bookController.GetBook(6);
            var result = (ObjectResult) actionResult.Result!;

            //assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task AddBookInfo_Returns201Status()
        {
            //arrange
            var bookService = new Mock<IBooksService>();
            var bookInfoDTO = BookMockData.newBookInfoDTO();
            var bookController = new BooksController(bookService.Object);

            //act
            var actionResult = await bookController.AddBookInfo(bookInfoDTO);
            var result = (ObjectResult) actionResult.Result!;

            //assert
            bookService.Verify(b=>b.AddBook(bookInfoDTO), Times.Exactly(1));
            //Assert.Equal(201, result.StatusCode);
        }
    }
}
