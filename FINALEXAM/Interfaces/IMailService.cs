using FINALEXAM.ViewModels;

namespace FINALEXAM.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestVM mailRequest);
    }
}
