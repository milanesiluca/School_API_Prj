using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class ModuleDto : ModuleManipulationDto
    {
        public IEnumerable<ActivityDto>? Activities { get; init; }
        [Required] public Guid Id { get; set; }
    }
}
