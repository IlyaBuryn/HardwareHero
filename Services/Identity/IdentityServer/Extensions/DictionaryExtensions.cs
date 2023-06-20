using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;

namespace IdentityServer.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static NameValueCollection AsNameValueCollection(this IDictionary<string, StringValues> collection)
        {
            var nv = new NameValueCollection();

            foreach (var field in collection)
            {
                nv.Add(field.Key, field.Value.First());
            }

            return nv;
        }       
    }
}
