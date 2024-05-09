using System.ComponentModel.DataAnnotations;

namespace RStudents.Models
{
    public class GroupDTO
    {
        public int Id {  get; set; }
        [Required, MaxLength(100)]
        public string GroupName { get; set; }
    }
}
