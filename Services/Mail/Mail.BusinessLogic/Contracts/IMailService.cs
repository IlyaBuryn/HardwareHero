namespace Mail.BusinessLogic.Contracts
{
    public interface IMailService
    {
        Guid SendMessage(MailMessageDto message);
    }
}
