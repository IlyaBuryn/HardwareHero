namespace Mail.BusinessLogic.Contracts
{
    public interface IMailServicePresets
    {
        Task<Guid> SendMailAsync(MailMessageDto mailMessage, MailPresets mailPresets);
    }
}
