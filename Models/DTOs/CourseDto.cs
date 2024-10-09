using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public record CourseDto
    {
        public Guid Id { get; set; }
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public DateTime Start { get; init; }
        public required IEnumerable<ModuleDto> Modules { get; init; }
    }
}
