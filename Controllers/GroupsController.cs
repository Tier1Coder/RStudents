using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RStudents.Models;
using RStudents.Services;

namespace RStudents.Controllers
{
    public class GroupsController : Controller
    {
        private readonly DatabaseContext context;
        public GroupsController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var groups = context.Groups.ToList();
            return View(groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupDTO groupDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(groupDTO);
            }

            var group = new Group
            {
                GroupName = groupDTO.GroupName
            };

            context.Groups.Add(group);
            context.SaveChanges();

            return RedirectToAction("Index", "Groups");
        }

        public IActionResult Edit(int id)
        {
            var group = context.Groups.Find(id);

            if (group == null)
            {
                return RedirectToAction("Index", "Groups");
            }

            var groupDTO = new GroupDTO 
            {
                GroupName = group.GroupName
            };

            ViewData["GroupId"] = group.GroupId;


            return View(groupDTO);

        }

        [HttpPost]
        public IActionResult Edit(int id, GroupDTO groupDTO)
        {
            var group = context.Groups.Find(id);
            if (group == null)
            {
                return RedirectToAction("Index", "Groups");
            }

            if (!ModelState.IsValid)
            {
                ViewData["GroupId"] = group.GroupId;

                return View(groupDTO);
            }

            group.GroupName = groupDTO.GroupName;

            context.SaveChanges();

            return RedirectToAction("Index", "Groups");

        }

        public IActionResult Delete(int id)
        {
            var group = context.Groups.Include(g => g.Students).FirstOrDefault(g => g.GroupId == id);
            if (group == null)
            {
                return RedirectToAction("Index", "Groups");
            }

            if (group.Students.Any())
            {
                TempData["Error"] = "Cannot delete the group because it has students assigned.";
                return RedirectToAction("Index", "Groups");
            }

            context.Groups.Remove(group);
            context.SaveChanges();

            return RedirectToAction("Index", "Groups");
        }


    }
}
