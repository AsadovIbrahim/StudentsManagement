using StudentManagement.Helpers;

namespace StudentManagement.Services.EmailService.Abstracts
{
    public interface IMailService
    {
        Task SendMailAsync(MailRequest mailRequest);
    }
}
