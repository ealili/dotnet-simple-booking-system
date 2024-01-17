using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Interfaces;
using SimpleBookingSystem.Services.Interfaces;
using SimpleBookingSystem.Utilities.Mail;

namespace SimpleBookingSystem.Services.Implementations;

public class BookingService: IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMailService _mailService;

    public BookingService(IBookingRepository bookingRepository, IMailService mailService)
    {
        _bookingRepository = bookingRepository;
        _mailService = mailService;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _bookingRepository.GetAllAsync(); 
    }

    public async Task<Booking> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        return booking;
    }

    public async Task AddAsync(Booking entity)
    {
        await _bookingRepository.AddAsync(entity);
        await _bookingRepository.SaveChangesAsync();
        
        // Send email to admin@admin.com
        // var mailData = new MailData
        // {
        //     EmailToName = "SimpleBookingSystem",
        //     EmailToId = "enesalili00@gmail.com",
        //     EmailSubject = "Booking Creation",
        //     EmailBody =  $"BOOKING WITH ID {entity.Id} HAS BEEN CREATED",
        // };
        // _mailService.SendMail(mailData);
        
        Console.WriteLine($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {entity.Id}");
    }
}