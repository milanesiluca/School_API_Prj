using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class ModuleForUpdateDto : ModuleManipulationDto
    {
        [Required] public Guid CourseId { get; set; }
    }
}
