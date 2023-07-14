using BookRentalSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BookRentalSystem.Repositories.IRepositories;
using BookRentalSystem.Repositories;
using BookRentalSystem.UnitOfWork;
using BookRentalSystem.Services.IServices;
using BookRentalSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BookRentalSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt=>opt.JsonSerializerOptions.DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddDbContext<BRSContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//add identity
builder.Services.AddIdentity<Customer, IdentityRole>()
    .AddEntityFrameworkStores<BRSContext>().AddDefaultTokenProviders();

//injecting dependencies
//RentBookService
builder.Services.AddScoped<IRentBookService, RentBookService>();
//Book service
builder.Services.AddScoped<IBooksService, BooksService>();
//Customer Service
builder.Services.AddScoped<ICustomersService, CustomersService>();
//Author Service
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
//Rental Service
builder.Services.AddScoped<IRentalsService, RentalsService>();
//BookAuthor Service
builder.Services.AddScoped<IBookAuthorsService, BookAuthorsService>();
//unit of work -> required by services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//For Books -> required by unit of work
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

builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Rental System", Version = "v1" }); option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme

                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

//Adding authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            //builder.WithOrigins("https://localhost:5173", "http://localhost:4200")
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


