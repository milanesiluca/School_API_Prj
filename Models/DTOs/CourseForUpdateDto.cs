using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class CourseForUpdateDto
    {
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public DateTime Start { get; init; }

    }
}
