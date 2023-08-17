using BookRentalSystem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add controllers
ServicesConfigurator.ConfigureControllers(builder.Services);
//Add db context
ServicesConfigurator.ConfigureDB(builder.Services, builder.Configuration);
//Add identity
ServicesConfigurator.ConfigureIdentity(builder.Services, builder.Configuration);

//injecting dependencies
DependenciesConfigurator.InjectDependencies(builder.Services);

builder.Services.AddEndpointsApiExplorer();

//authorize button in swagger UI
AuthorizationConfig.Authorization(builder.Services);

//Adding authentication
AuthenticationConfigurator.ConfigureAuthentication(builder.Services, builder.Configuration);

//Add cors
CrossOriginConfiguration.CorsConfig(builder.Services);

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


