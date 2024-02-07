using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Shared.Responses
{
    public class IdentityResultResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
