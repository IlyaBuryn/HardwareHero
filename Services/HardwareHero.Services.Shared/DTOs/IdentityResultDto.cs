using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Services.Shared.DTOs
{
    public class IdentityResultDto
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
