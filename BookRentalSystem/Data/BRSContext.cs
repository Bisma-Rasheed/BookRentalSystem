using Microsoft.EntityFrameworkCore;
using BookRentalSystem.Models;

namespace BookRentalSystem.Data
{
    public class BRSContext : DbContext
    {
        public BRSContext(DbContextOptions<BRSContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
    }
}
