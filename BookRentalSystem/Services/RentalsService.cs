﻿using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.UnitOfWork;

namespace BookRentalSystem.Services
{
    public class RentalsService : Service<Rental>, IRentalsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalsService(IUnitOfWork unitOfWork, IGenericRepo<Rental> _repo) : base(_repo)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Rental> AddRental(RentalDTO rentalDTO)
        {
            return await _unitOfWork.RentalRepository.AddRentalInfo(rentalDTO);
        }
        public async Task UpdateRental(int id, RentalDTO rentalDTO)
        {
            await _unitOfWork.RentalRepository.UpdateRentalInfo(id, rentalDTO);
            return;
        }  
    }
}