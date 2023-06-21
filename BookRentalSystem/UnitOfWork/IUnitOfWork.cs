using BookRentalSystem.Repositories.IRepositories;

namespace BookRentalSystem.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBookRepo BookRepository { get; }
        ICustomerRepo CustomerRepository { get; } 
        IRentalRepo RentalRepository { get; }
        IAuthorRepo AuthorRepository { get; }
        IBookAuthorRepo BookAuthorRepository { get; }

        Task Save();
    }
}
