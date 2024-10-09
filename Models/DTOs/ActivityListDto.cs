using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class ActivityListDto
    {
        [Required] public string? Name { get; set; }
        [Required] public string? Description { get; set; }
        [Required] public string? ActivityType { get; set; }
        [Required] public DateTime? Start { get; set; }
        [Required] public DateTime? End { get; set; }
        [Required] public Guid ModuleId { get; set; }
    }
}
