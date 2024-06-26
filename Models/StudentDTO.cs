﻿using System.ComponentModel.DataAnnotations;

namespace RStudents.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = "";

        [Required, MaxLength(100)]
        public string LastName { get; set; } = "";
        [Required, Range(15, 100)]
        public int Age { get; set; } = 18;
        [Required]
		public int GroupId { get; set; }

	}
}
