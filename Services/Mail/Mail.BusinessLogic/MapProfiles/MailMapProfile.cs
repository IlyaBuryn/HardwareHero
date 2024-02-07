namespace Mail.BusinessLogic.MapProfiles
{
    public class MailMapProfile : Profile
    {
        public MailMapProfile()
        {
            CreateMap<MailMessage, MailMessageDto>()
                .ReverseMap();
        }
    }
}
