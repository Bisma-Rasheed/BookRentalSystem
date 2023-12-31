﻿using BookRentalSystem.Data;
using BookRentalSystem.Models;
using BookRentalSystem.Models.DTO.ModelDTOs;
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
            return rental!;
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCustomer(string id)
        {
            var rentals = await _dbSet.Where(r => r.CustomerID == id)
                .Include(r => r.Book).Include(r => r.Customer).ToListAsync();
            return rentals;
        }
        public async Task<Rental> AddRentalInfo(RentalDTO rentalDTO)
        {
            try
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
            catch (Exception) { throw; }
            
        }

        public async Task UpdateRentalInfo(int id, RentalDTO rentalDTO)
        {
            try
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
            catch (Exception) { throw; }
        }
    }
}
