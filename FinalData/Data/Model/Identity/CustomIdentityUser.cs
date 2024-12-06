using Microsoft.AspNetCore.Identity;
namespace FinalData.Data.Model.Identity
{
    public class CustomIdentityUser : IdentityUser
    {
        public string? DeportePrac { get; set; }
    }
}