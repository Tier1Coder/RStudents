using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RStudents.Models
{
    [Table("Groups")]
    public class Group
    {
        public Group() 
        { 
            Students = new HashSet<Student>(); 
        }

        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }
}
