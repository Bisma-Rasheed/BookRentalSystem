using BookRentalSystem.Data;
using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookRentalSystem.Repositories
{
    public class RentalRepo : GenericRepo<Rental>, IRentalRepo
    {
        public RentalRepo(BRSContext context) : base(context) { }

        public new async Task<IEnumerable<Rental>> GetAll()
        {
            return await _dbSet.Include(r => r.Book).Include(r => r.Customer).ToListAsync();
        }

        public new async Task<Rental> GetById(int id)
        {
            //eager loading using include
            Rental? rental = await _dbSet.Where(r => r.RentalID == id).Include(r => r.Book).Include(r => r.Customer).SingleOrDefaultAsync();
            return rental;
        }
        public async Task<Rental> AddRentalInfo(RentalDTO rentalDTO)
        {
            Rental rental = new()
            {
                BookID = rentalDTO.BookID,
                CustomerID = rentalDTO.CustomerID,
                RentalDate = rentalDTO.RentalDate,
                ReturnDate = rentalDTO.ReturnDate,
                LateFee = rentalDTO.LateFee
            };

            _dbSet.Add(rental);
            await Save();
            return rental;
        }


        public async Task UpdateRentalInfo(int id, RentalDTO rentalDTO)
        {
            var rental = await GetById(id);
            rental.BookID = rentalDTO.BookID;
            rental.CustomerID = rentalDTO.CustomerID;
            rental.RentalDate = rentalDTO.RentalDate;
            rental.ReturnDate = rentalDTO.ReturnDate;
            rental.LateFee = rentalDTO.LateFee;

            UpdateDB(rental);
            await Save();
            return;
        }
    }
}
