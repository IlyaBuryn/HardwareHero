using Mail.DTOs.Events;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Presets
{
    public class NewContributorPreset : Preset
    {
        public NewContributorPreset(IConfiguration configuration)
            : base(configuration.GetSection("ContributorApplicationPagePath").Value)
        { }

        protected override void SetupReplacedProperties(SendMailEvent message)
        {
            base.SetupReplacedProperties(message);
        }

        protected override void SetupTitle(SendMailEvent message)
        {
            message.Subject = "HardwareHero.com: Contributor application";
        }
    }
}
