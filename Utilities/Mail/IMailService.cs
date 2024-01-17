namespace SimpleBookingSystem.Utilities.Mail;

public interface IMailService
{
    bool SendMail(MailData mailData);
}