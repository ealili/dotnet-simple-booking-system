using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Data;
using SimpleBookingSystem.DTOs;
using SimpleBookingSystem.Infrastructure;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Implementations;
using SimpleBookingSystem.Repositories.Interfaces;
using SimpleBookingSystem.Services.Implementations;
using SimpleBookingSystem.Services.Interfaces;
using SimpleBookingSystem.Utilities.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
if (allowedOrigin != null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("myAppCors", policy =>
        {
            policy.WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

// builder.Services.AddControllers();

builder.Services.AddScoped<IValidator<BookingDto>, BookingDtoValidator>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("InMemoryDatabase"); });

builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IMailService, MailService>();


builder.Services.AddExceptionHandler<FluentValidationExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

// Seed the in-memory database with initial data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    if (!dbContext.Resources.Any())
    {
        dbContext.Resources.AddRange(
            new Resource { Name = "Resource 1", Quantity = 1 },
            new Resource { Name = "Resource 2", Quantity = 5 },
            new Resource { Name = "Resource 3", Quantity = 200 },
            new Resource { Name = "Resource 4", Quantity = 15 },
            new Resource { Name = "Resource 5", Quantity = 20 }
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

app.UseCors("myAppCors");

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();