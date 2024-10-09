using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.DTOs
{
    public class ActivityPutDto
    {
        //[Required] public Guid Id { get; set; }
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public Guid TypeId { get; init; }
        [Required] public DateTime? Start { get; init; }
        [Required] public DateTime? End { get; init; }
        [Required] public Guid ModuleId { get; init; }
    }
}
