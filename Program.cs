using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Data;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Implementations;
using SimpleBookingSystem.Repositories.Interfaces;
using SimpleBookingSystem.Services.Implementations;
using SimpleBookingSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    // options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseInMemoryDatabase("InMemoryDatabase");
});


builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

var app = builder.Build();

// Seed the in-memory database with initial data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    if (!dbContext.Resources.Any())
    {
        dbContext.Resources.AddRange(
            new Resource { Name = "Resource1", Quantity = 1 },
            new Resource { Name = "Resource2", Quantity = 5 },
            new Resource { Name = "Resource3", Quantity = 2 }
        );
        dbContext.SaveChanges();
    }
    
    
    if (!dbContext.Bookings.Any())
    {
        dbContext.Bookings.AddRange(
            new Booking
            {
                FromDateTime = DateTime.UtcNow,
                ToDateTime = DateTime.UtcNow.AddHours(2),
                BookedQuantity = 5
            },
            new Booking
            {
                FromDateTime = DateTime.UtcNow.AddDays(1),
                ToDateTime = DateTime.UtcNow.AddDays(1).AddHours(3),
                BookedQuantity = 8
            }
            // Add more bookings as needed
        );
        dbContext.SaveChanges();
    }
}

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
