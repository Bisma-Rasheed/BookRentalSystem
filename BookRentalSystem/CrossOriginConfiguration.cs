namespace BookRentalSystem
{
    public static class CrossOriginConfiguration
    {
        public static void CorsConfig(IServiceCollection services)
        {
            services.AddCors(options =>
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
        }
    }
}
