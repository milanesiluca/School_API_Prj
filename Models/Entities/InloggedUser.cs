using System.ComponentModel.DataAnnotations;

namespace API_School_own_prj.Models.Entities
{
    public class InloggedUser : User
    {
        [Required] public string? Token {  get; set; }
    }
}
