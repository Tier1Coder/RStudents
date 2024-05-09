using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RStudents.Models;
using RStudents.Services;

namespace RStudents.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupDetailsController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public GroupDetailsController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/GroupDetails/5
		[HttpGet("{id}")]
		public async Task<ActionResult<GroupWithStudentsDTO>> GetGroupWithStudents(int id)
		{
			var groupWithStudents = await _context.Groups
				.Where(g => g.GroupId == id)
				.Select(g => new GroupWithStudentsDTO
				{
					GroupId = g.GroupId,
					Name = g.GroupName,
					Students = g.Students.Select(s => new StudentDTO
					{
						FirstName = s.FirstName,
						LastName = s.LastName,
						Age = s.Age,
						GroupId = s.GroupId
					}).ToList()
				})
				.FirstOrDefaultAsync();

			if (groupWithStudents == null)
			{
				return NotFound();
			}

			return groupWithStudents;
		}
	}
}
