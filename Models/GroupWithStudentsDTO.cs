using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RStudents.Models
{
	public class GroupWithStudentsDTO
	{
		public int GroupId { get; set; }
		public string Name { get; set; }
		public List<StudentDTO> Students { get; set; }
	}

}
