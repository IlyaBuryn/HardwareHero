using System.Reflection;

namespace KafkaEventStream.Contracts
{
    public abstract class EventEndpointManager
    {
        public abstract Task<string> InvokeByEndpoint(string endpoint);

        public bool IsMatchEndpoints(string endpoint, string template)
        {
            var endpointParts = endpoint.Split('/');
            var templateParts = template.Split('/');
            if (endpointParts.Length != templateParts.Length)
            {
                return false;
            }

            for (int i = 0; i < endpointParts.Length; i++)
            {
                if (!templateParts[i].StartsWith("{") && templateParts[i] != endpointParts[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsMatchEndpointsAndPopulate<T>(string endpoint, string template, out T result) where T : new()
        {
            result = new T();
            var endpointParts = endpoint.Split('/');
            var templateParts = template.Split('/');
            if (endpointParts.Length != templateParts.Length)
            {
                return false;
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < endpointParts.Length; i++)
            {
                if (templateParts[i].StartsWith("{") && templateParts[i].EndsWith("}"))
                {
                    var propertyName = templateParts[i].Trim('{', '}');
                    var property = properties.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

                    if (property != null)
                    {
                        try
                        {
                            var value = Convert.ChangeType(endpointParts[i], property.PropertyType);
                            property.SetValue(result, value);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                else if (templateParts[i] != endpointParts[i])
                {
                    result = default;
                    return false;
                }
            }

            return true;
        }
    }
}
