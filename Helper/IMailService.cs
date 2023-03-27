using DotnetWebApi.Models;

namespace DotnetWebApi.Helper
{
    public interface IMailService
    {
        Task SendEmailiAsync(MailRequest mailRequest);
    }
}