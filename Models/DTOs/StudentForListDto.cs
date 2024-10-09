using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class StudentForListDto
    {
        [Required] public Guid Id { get; init; }
        [Required]public string? Name { get; init; }

        [Required]
        public string? Surname { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; init; }
        public string? CourseID { get; set; }
    }
}
