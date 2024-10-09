using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
        public string? Role { get; set; }
        [Required] string? HashedPwd {  get; set; }
    }
}
