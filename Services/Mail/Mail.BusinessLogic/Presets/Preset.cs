using Mail.DTOs.Mail;

namespace Mail.BusinessLogic.Presets
{
    public abstract class Preset
    {
        private string? _filePath;
        private Dictionary<string, string> _replacedProperties;
        private string _title = string.Empty;

        protected Preset(string filePath)
        {
            _filePath = filePath;
            _replacedProperties = new();
        }

        protected virtual void SetupTitle(MailMessageDto message)
        {
            message.Subject = "[HardwareHero]";
        }

        protected virtual void SetupReplacedProperties(MailMessageDto message)
        {
            AddReplacedProperty("<<<timestamp>>>", message.MailSettingsEvent?.Timestamp.ToString("dd:MM:yyyy - HH:mm") 
                ?? DateTime.Now.ToString("dd:MM:yyyy - HH:mm"));
            AddReplacedProperty("<<<username>>>", message.MailSettingsEvent?.Username ?? "{username-not-found}");
        }

        public (MailMessageDto, bool) CustomizeMessageBody(MailMessageDto message)
        {
            if (!File.Exists(_filePath))
            {
                return (message, false);
            }

            SetupReplacedProperties(message);
            SetupTitle(message);

            if (_replacedProperties == null || _replacedProperties.Count == 0)
            {
                return (message, false);
            }

            var text = File.ReadAllText(_filePath);

            foreach (var prop in _replacedProperties)
            {
                text = text.Replace(prop.Key, prop.Value);
            }
            message.Body = text;

            return (message, true);
        }

        protected void AddReplacedProperty(string oldStr, string newStr)
        {
            if (_replacedProperties.ContainsKey(oldStr))
            {
                _replacedProperties[oldStr] = newStr;
            }
            else
            {
                _replacedProperties.Add(oldStr, newStr);
            }
        }
    }
}
