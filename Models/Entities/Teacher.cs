using Microsoft.AspNetCore.Identity;

namespace API_School_own_prj.Models.Entities
{
    public class Teacher : User
    {
        
        public Guid? ModuleId { get; set; } = Guid.Empty;
        public Module? Module { get; set; }
        

    }
}
