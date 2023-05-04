using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Mail;
using HardwareHero.Services.Shared.Models.Mail;

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
