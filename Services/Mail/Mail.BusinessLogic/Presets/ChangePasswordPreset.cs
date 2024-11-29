using Mail.DTOs.Events;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Presets
{
    public class ChangePasswordPreset : Preset
    {
        public ChangePasswordPreset(IConfiguration configuration)
            : base(configuration.GetSection("ChangePasswordPagePath").Value)
        { }

        protected override void SetupReplacedProperties(SendMailEvent message)
        {
            base.SetupReplacedProperties(message);
        }

        protected override void SetupTitle(SendMailEvent message)
        {
            message.Subject = "HardwareHero.com: your password has been changed";
        }
    }
}
