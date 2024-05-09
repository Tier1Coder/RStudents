using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RStudents.Models;
using RStudents.Services;

namespace RStudents.Controllers
{
	[Route("api/GroupsApi")]
	[ApiController]
	public class GroupsApiController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public GroupsApiController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/GroupsApi
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GroupDTO>>> GetAllGroups()
		{
			var groups = await _context.Groups
				.Select(g => new GroupDTO
				{
					Id = g.GroupId,  
					GroupName = g.GroupName
				})
				.ToListAsync();

			return groups;
		}


		// GET: api/GroupsApi/5
		[HttpGet("{id}")]
		public async Task<ActionResult<GroupDTO>> GetGroup(int id)
		{
			var groupDto = await _context.Groups
				.Where(g => g.GroupId == id)
				.Select(g => new GroupDTO
				{
					Id = g.GroupId,
					GroupName = g.GroupName
				})
				.FirstOrDefaultAsync();

			if (groupDto == null)
			{
				return NotFound();
			}

			return groupDto;
		}

		// POST: api/GroupsApi
		[HttpPost]
		public async Task<ActionResult<GroupDTO>> PostGroup(GroupDTO groupDto)
		{
			var group = new Group
			{
				GroupName = groupDto.GroupName
			};

			_context.Groups.Add(group);
			await _context.SaveChangesAsync();

			groupDto.Id = group.GroupId;
			return CreatedAtAction("GetGroup", new { id = group.GroupId }, groupDto);
		}

		// PUT: api/GroupsApi/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutGroup(int id, GroupDTO groupDto)
		{
			if (id != groupDto.Id)
			{
				return BadRequest("Mismatched ID in the DTO and URL.");
			}

			var group = await _context.Groups.FindAsync(id);
			if (group == null)
			{
				return NotFound();
			}

			group.GroupName = groupDto.GroupName;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Groups.Any(g => g.GroupId == id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// DELETE: api/GroupsApi/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGroup(int id)
		{
			var group = await _context.Groups.Include(g => g.Students).FirstOrDefaultAsync(g => g.GroupId == id);
			if (group == null)
			{
				return NotFound();
			}

			if (group.Students.Any())
			{
				return BadRequest(new { message = "Cannot delete group because it has students assigned." });
			}

			_context.Groups.Remove(group);
			await _context.SaveChangesAsync();

			return Ok(new { message = "Group deleted successfully." });
		}
	}
}
