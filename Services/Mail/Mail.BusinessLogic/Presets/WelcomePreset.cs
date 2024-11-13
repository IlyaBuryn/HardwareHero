using Mail.DTOs.Mail;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Presets
{
    public class WelcomePreset : Preset
    {
        public WelcomePreset(IConfiguration configuration) 
            : base(configuration.GetSection("WelcomePagePath").Value)
        { }

        protected override void SetupReplacedProperties(MailMessageDto message)
        {
            base.SetupReplacedProperties(message);

            AddReplacedProperty("<<<username>>>", message.RecipientId.ToString());
        }

        protected override void SetupTitle(MailMessageDto message)
        {
            message.Subject = "Welcome to HardwareHero!";
        }
    }
}
