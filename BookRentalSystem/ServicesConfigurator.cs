using BookRentalSystem.Data;
using BookRentalSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BookRentalSystem
{
    public static class ServicesConfigurator
    {
        public static void ConfigureDB(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BRSContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<Customer, IdentityRole>()
                    .AddEntityFrameworkStores<BRSContext>().AddDefaultTokenProviders();

        }

        public static void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
        }
    }
}
