using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class ActivityTypeDto
    {
        [Required] public string? Name { get; set; }
    }
}
