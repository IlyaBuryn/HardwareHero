using Mail.DTOs.Events;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Presets
{
    public class WelcomePreset : Preset
    {
        public WelcomePreset(IConfiguration configuration) 
            : base(configuration.GetSection("WelcomePagePath").Value)
        { }

        protected override void SetupReplacedProperties(SendMailEvent message)
        {
            base.SetupReplacedProperties(message);
        }

        protected override void SetupTitle(SendMailEvent message)
        {
            message.Subject = "Welcome to HardwareHero!";
        }
    }
}
