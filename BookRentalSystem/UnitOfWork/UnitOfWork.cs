using BookRentalSystem.Data;
using BookRentalSystem.Repositories.IRepositories;

namespace BookRentalSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BRSContext _context;
        private IBookRepo _bookRepo;
        private ICustomerRepo _customerRepo;
        private IAuthorRepo _authorRepo;
        private IRentalRepo _rentalRepo;
        private IBookAuthorRepo _bookAuthorRepo;

        public UnitOfWork(BRSContext context, IBookRepo bookRepo, ICustomerRepo customerRepo, IAuthorRepo authorRepo, IRentalRepo rentalRepo, IBookAuthorRepo bookAuthorRepo)
        {
            _context = context;
            _bookRepo = bookRepo;
            _customerRepo = customerRepo;
            _authorRepo = authorRepo;
            _rentalRepo = rentalRepo;
            _bookAuthorRepo = bookAuthorRepo;
        }

        public IBookRepo BookRepository => _bookRepo;
        public ICustomerRepo CustomerRepository => _customerRepo;
        public IRentalRepo RentalRepository => _rentalRepo;
        public IAuthorRepo AuthorRepository => _authorRepo;
        public IBookAuthorRepo BookAuthorRepository => _bookAuthorRepo;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
