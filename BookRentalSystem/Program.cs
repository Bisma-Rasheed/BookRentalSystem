using BookRentalSystem.Data;
using BookRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Repositories;
using BookRentalSystem.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt=>opt.JsonSerializerOptions.DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddDbContext<BRSContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//injecting dependencies 
//unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//For Books
builder.Services.AddScoped<IBookRepo, BookRepo>();
//For Customers
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
//For Authors
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
//For Rentals
builder.Services.AddScoped<IRentalRepo, RentalRepo>();
//For BookAuthors
builder.Services.AddScoped<IBookAuthorRepo, BookAuthorRepo>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
