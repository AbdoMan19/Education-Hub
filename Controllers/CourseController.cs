using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Task.DB.Models;
using MVC_Task.Services.CourseService;
using MVC_Task.Services.DepartmentService;

namespace MVC_Task.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IDepartmentService _departmentService;

        public CourseController(ICourseService courseService , IDepartmentService departmentService)
        {
            _courseService = courseService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAll();
            return View(courses.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.Get(id);
            if (course.IsSuccess == false)
            {
                return NotFound();
            }
            return View(course.Data);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.Add(course);
                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.Get(id);
            if (course.Data == null)
            {
                return NotFound();
            }
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            return View(course.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (ModelState.IsValid)
            {
                var updateResult = await _courseService.Update(id, course);
                if (!updateResult.IsSuccess)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.Get(id);
            if (course.Data == null)
            {
                return NotFound();
            }
            return View(course.Data);
        }

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
