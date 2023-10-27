using BookRentalSystem;
using BookRentalSystem.Models.EmailConfig;
using BookRentalSystem.Services;
using BookRentalSystem.Services.IServices;

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


//Add email config
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailService, EmailService>();
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


