using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Repositories;
using BookRentalSystem.UnitOfWork;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Services;

namespace BookRentalSystem
{
    public static class DependenciesConfigurator
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddScoped<IRentBookService, RentBookService>();
            //Book service
            services.AddScoped<IBooksService, BooksService>();
            //Customer Service
            services.AddScoped<ICustomersService, CustomersService>();
            //Author Service
            services.AddScoped<IAuthorsService, AuthorsService>();
            //Rental Service
            services.AddScoped<IRentalsService, RentalsService>();
            //BookAuthor Service
            services.AddScoped<IBookAuthorsService, BookAuthorsService>();
            //unit of work -> required by services
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            //For Books -> required by unit of work
            services.AddScoped<IBookRepo, BookRepo>();
            //For Customers
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            //For Authors
            services.AddScoped<IAuthorRepo, AuthorRepo>();
            //For Rentals
            services.AddScoped<IRentalRepo, RentalRepo>();
            //For BookAuthors
            services.AddScoped<IBookAuthorRepo, BookAuthorRepo>();
        }
    }
}
