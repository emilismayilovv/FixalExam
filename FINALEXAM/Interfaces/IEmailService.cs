namespace FINALEXAM.Interfaces
{
    public interface IEmailService
    {
        Task SendMail(string mail, string subject, string body, bool isHtml = false);
    }
}
