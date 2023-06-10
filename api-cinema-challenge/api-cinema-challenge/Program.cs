using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using cinema.endpoints;
using api_cinema_challenge.Models.Customer;
using human.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICustomer,  Customer>();
builder.Services.AddScoped<IDatabaseRepository<Customer>, DatabaseRepository<Customer>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// @TODO change the capitalized strings in the options to match your api and contact details
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TITLE_OF_PROJECT_API",
        Description = "DESCRIPTION_OF_API",
        Contact = new OpenApiContact
        {
            Name = "YOUR_NAME",
        }
    });
});

// @TODO Uncomment next 2 lines and add your context class here
//builder.Services.AddDbContext<DB_CONTEXT_NAME_HERE>(
//    o => o.UseNpgsql(builder.Configuration.GetConnectionString("CinemaDBConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomer();
app.ConfigureCustomer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
