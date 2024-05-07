using System.ComponentModel.DataAnnotations;

namespace RStudents.Models
{
    public class GroupDTO
    {
        [Required, MaxLength(100)]
        public string GroupName { get; set; }
    }
}
