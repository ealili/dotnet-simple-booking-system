using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace SimpleBookingSystem.DTOs;

public class BookingDto
{
    public DateTime FromDateTime { get; set; }
        
    public DateTime ToDateTime { get; set; }
        
    public int BookedQuantity { get; set; }
        
    public int ResourceId { get; set; }
}


public class BookingDtoValidator : AbstractValidator<BookingDto>
{
    public BookingDtoValidator()
    {
        RuleFor(b => b.FromDateTime)
            .NotEmpty().WithMessage("FromDateTime cannot be empty")
            .Must(date => date.Date >= DateTime.Today)
            .WithMessage("FromDateTime must be from today or a future date");

        RuleFor(b => b.ToDateTime)
            .NotEmpty().WithMessage("ToDateTime cannot be empty")
            .GreaterThan(b => b.FromDateTime).WithMessage("ToDateTime must be greater than FromDateTime");

        RuleFor(b => b.BookedQuantity)
            .NotEmpty().WithMessage("BookedQuantity cannot be empty")
            .GreaterThanOrEqualTo(1).WithMessage("BookedQuantity must be greater than or equal to 1");

        RuleFor(b => b.ResourceId)
            .NotEmpty().WithMessage("ResourceId cannot be empty");
    }
}