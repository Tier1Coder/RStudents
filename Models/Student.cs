using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RStudents.Models
{
    public class Student
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; } = "";

        [MaxLength(100)]
        public string LastName { get; set; } = "";
        [Range(15, 100)]
        public int Age { get; set; } = 18;
    }
}
