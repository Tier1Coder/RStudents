using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RStudents.Models;
using RStudents.Services;

namespace RStudents.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsApiController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public StudentsApiController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/StudentsApi
		[HttpGet]
		public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
		{
			var students = await _context.Students.Include(s => s.Group).ToListAsync();
			var studentDTOs = students.Select(s => new StudentDTO
			{
				FirstName = s.FirstName,
				LastName = s.LastName,
				Age = s.Age,
				GroupId = s.GroupId,
			}).ToList();

			return studentDTOs;
		}

		// GET: api/StudentsApi/5
		[HttpGet("{id}")]
		public async Task<ActionResult<StudentDTO>> GetStudent(int id)
		{
			var student = await _context.Students.Include(s => s.Group).FirstOrDefaultAsync(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			var studentDTO = new StudentDTO
			{
				FirstName = student.FirstName,
				LastName = student.LastName,
				Age = student.Age,
				GroupId = student.GroupId,
			};

			return studentDTO;
		}


		// POST: api/StudentsApi
		[HttpPost]
		public async Task<ActionResult<StudentDTO>> PostStudent(StudentDTO studentDTO)
		{
			var student = new Student
			{
				FirstName = studentDTO.FirstName,
				LastName = studentDTO.LastName,
				Age = studentDTO.Age,
				GroupId = studentDTO.GroupId
			};

			_context.Students.Add(student);
			await _context.SaveChangesAsync();

			studentDTO.Id = student.Id; // Update DTO with new ID
			return CreatedAtAction("GetStudent", new { id = student.Id }, studentDTO);
		}


		// DELETE: api/StudentsApi/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStudent(int id)
		{
			var student = await _context.Students.FindAsync(id);
			if (student == null)
			{
				return NotFound(new { message = "Student not found." });
			}

			_context.Students.Remove(student);
			await _context.SaveChangesAsync();

			return Ok(new { message = "Student deleted successfully." }); // Zwraca status 200 OK z komunikatem
		}


		// PUT: api/StudentsApi/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutStudent(int id, StudentDTO studentDTO)
		{
			if (id != studentDTO.Id)
			{
				return BadRequest();
			}

			var student = await _context.Students.FindAsync(id);
			if (student == null)
			{
				return NotFound();
			}

			student.FirstName = studentDTO.FirstName;
			student.LastName = studentDTO.LastName;
			student.Age = studentDTO.Age;
			student.GroupId = studentDTO.GroupId;

			_context.Entry(student).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Students.Any(e => e.Id == id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok(new { message = "Student modified successfully." });
		}
		// POST: api/Students/AddStudentWithGroup
		[HttpPost("AddStudentWithGroup")]
		public async Task<ActionResult<Student>> AddStudentWithGroup(StudentWithGroupDTO studentWithGroupDTO)
		{
			var group = await _context.Groups
				.FirstOrDefaultAsync(g => g.GroupName == studentWithGroupDTO.GroupName);

			if (group == null)
			{
				group = new Group { GroupName = studentWithGroupDTO.GroupName };
				_context.Groups.Add(group);
				await _context.SaveChangesAsync();
			}

			var student = new Student
			{
				FirstName = studentWithGroupDTO.FirstName,
				LastName = studentWithGroupDTO.LastName,
				Age = studentWithGroupDTO.Age,
				GroupId = group.GroupId
			};

			_context.Students.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStudent", new { id = student.Id }, student);
		}

	}
}
