using Mail.DTOs.Mail;

namespace Mail.BusinessLogic.Contracts
{
    public interface IMailService
    {
        Task<Guid> SendMailAsync(MailMessageDto messageToSend);
    }
}
