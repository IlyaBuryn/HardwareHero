using Mail.DTOs.Events;

namespace Mail.BusinessLogic.Contracts
{
    public interface IMailServicePresets : IMailService
    {
        Task<Guid?> SendMailTemplateAsync(SendMailEvent mailEvent);
    }
}
