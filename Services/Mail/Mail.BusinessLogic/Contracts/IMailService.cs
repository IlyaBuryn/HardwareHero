using Mail.DTOs.Events;

namespace Mail.BusinessLogic.Contracts
{
    public interface IMailService
    {
        Task<Guid?> SendMailAsync(SendMailEvent messageToSend);
        Task<Guid?> SaveMailAsync(SendMailEvent messageToSend);
    }
}
