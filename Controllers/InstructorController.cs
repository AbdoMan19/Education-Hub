using Microsoft.AspNetCore.Mvc;
using MVC_Task.Services.InstructorService;
using MVC_Task.DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Task.Services.DepartmentService;
using MVC_Task.Services.CourseService;
using Azure;

namespace MVC_Task.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;
        private readonly ICourseService _courseService;

        public InstructorController(IInstructorService instructorService , IDepartmentService departmentService , ICourseService courseService)
        {
            _instructorService = instructorService;
            _departmentService = departmentService;
            _courseService = courseService;

        }

        public async Task<IActionResult> Index()
        {
            var result = await _instructorService.GetAll();
            return View(result.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _instructorService.Get(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return View(result.Data);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAll();
            var courses = await _courseService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            ViewBag.Courses = new SelectList(courses.Data.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                var result = await _instructorService.Add(instructor);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.ErrorList)
                {
                    ModelState.AddModelError(error.PropertyName, error.Message);
                }
            }
            var departments = await _departmentService.GetAll();
            var courses = await _courseService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            ViewBag.Courses = new SelectList(courses.Data.ToList(), "Id", "Name");
            return View(instructor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _instructorService.Get(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            var departments = await _departmentService.GetAll();
            var courses = await _courseService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            ViewBag.Courses = new SelectList(courses.Data.ToList(), "Id", "Name");
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                var result = await _instructorService.Update(id, instructor);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.ErrorList)
                {
                    ModelState.AddModelError(error.PropertyName, error.Message);
                }
            }
            var departments = await _departmentService.GetAll();
            var courses = await _courseService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            ViewBag.Courses = new SelectList(courses.Data.ToList(), "Id", "Name");
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _instructorService.Get(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _instructorService.Delete(id);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();


        }
    }
}
