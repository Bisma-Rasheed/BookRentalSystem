using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO;
using BookRentalSystem.Models.DTO.ModelDTOs;
using BookRentalSystem.Models.DTO.ViewModelDTOs;

namespace BookRentalSystem.TestApi.MockData
{
    public class BookMockData
    {
        public static IEnumerable<BookDTO> GetBooks()
        {
            return new List<BookDTO>
            {
                new BookDTO
                {
                    Title = "Harry Potter",
                    Description = "Description",
                    isAvailable = "Yes",
                    RentalPrice = 1.5M,
                    Quantity = 2,
                },
                new BookDTO
                {
                    Title = "Barry Potter",
                    Description = "Description",
                    isAvailable = "No",
                    RentalPrice = 3.5M,
                    Quantity = 5,
                },
                new BookDTO
                {
                    Title = "Garry Potter",
                    Description = "Description",
                    isAvailable = "No",
                    RentalPrice = 4.5M,
                    Quantity = 4,
                }
            };
        }

        public static IEnumerable<BookDTO> AvailableBooks() 
        {
            return new List<BookDTO>
            {
                new BookDTO
                {
                    Title = "Harry Potter",
                    Description = "Description",
                    isAvailable = "Yes",
                    RentalPrice = 1.5M,
                    Quantity = 2,
                }
            };
        }

        public static Book GetById(int id)
        {
            return new Book
            {
                BookID = 1,
                Title = "Harry Potter",
                Description = "Description",
                isAvailable = "Yes",
                RentalPrice = 1.5M,
                Quantity = 2
            };
        }

        public static BookInfoDTO newBookInfoDTO()
        {
            return new BookInfoDTO
            {
                BookName = "Harry Potter",
                About_Book = "very good book",
                Author = "J.K.Rowling",
                About_Author = "fiction author",
                isAvailable = "No",
                RentalPrice = 1.1m,
                Quantity = 2
            };
        }
    }
}
