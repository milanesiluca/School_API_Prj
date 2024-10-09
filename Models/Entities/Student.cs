using Microsoft.AspNetCore.Identity;

namespace API_School_own_prj.Models.Entities
{
    public class Student : User
    {
        public Guid? CourseId { get; set; } = Guid.Empty;
        public Course? Course { get; set; }

    }
}
