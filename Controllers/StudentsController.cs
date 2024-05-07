using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RStudents.Models;
using RStudents.Services;

namespace RStudents.Models
{
    public class StudentsController : Controller
    {
        private readonly DatabaseContext context;
        public StudentsController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
			var students = context.Students.Include(s => s.Group).ToList(); // eager
			return View(students);
        }

        public IActionResult Create() 
        {
            ViewData["Groups"] = new SelectList(context.Groups.ToList(), "GroupId", "GroupName");
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Groups"] = new SelectList(context.Groups.ToList(), "GroupId", "GroupName");
                return View(studentDTO);
            }

            var student = new Student
            {
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Age = studentDTO.Age,
                GroupId = studentDTO.GroupId
            };

            context.Students.Add(student);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }


        public IActionResult Edit(int id) 
		{
			var student = context.Students.Include(s => s.Group).FirstOrDefault(s => s.Id == id);

			if (student == null)
			{
				return RedirectToAction("Index", "Students");
			}

			var studentDTO = new StudentDTO()
			{
				FirstName = student.FirstName,
				LastName = student.LastName,
				Age = student.Age,
				GroupId = student.GroupId
			};

			ViewData["StudentId"] = student.Id;
			ViewData["Groups"] = new SelectList(context.Groups, "GroupId", "GroupName");



			return View(studentDTO);

		}

		[HttpPost]
		public IActionResult Edit(int id, StudentDTO studentDTO)
		{
			var student = context.Students.Find(id);
			if (student == null)
			{
				return RedirectToAction("Index", "Students");
			}

			if (!ModelState.IsValid)
			{
				ViewData["StudentId"] = student.Id;
				ViewData["Groups"] = new SelectList(context.Groups, "GroupId", "GroupName");


				return View(studentDTO); 
			}

			student.FirstName = studentDTO.FirstName;
			student.LastName = studentDTO.LastName;
			student.Age = studentDTO.Age;
			student.GroupId = studentDTO.GroupId;

			context.SaveChanges();

			return RedirectToAction("Index", "Students");

		}

		public IActionResult Delete(int id)
		{
			var student = context.Students.Find(id);
			if (student == null)
			{
				return RedirectToAction("Index", "Students");
			}

			context.Students.Remove(student);
			context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

	}
}
