using HardwareHero.Services.Shared.DTOs.Mail;
using System.Net.Mail;

namespace Mail.BusinessLogic.Contracts
{
    public interface IMailService
    {
        Task<bool> SendMessage(MailMessageDto message);
    }
}
