using Microsoft.AspNetCore.Mvc;
using MVC_Task.Services.DepartmentService;
using MVC_Task.DB.Models;

namespace MVC_Task.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _departmentService.GetAll();
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _departmentService.Get(id);
            if (response == null) return NotFound();
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid) return View(department);
            await _departmentService.Add(department);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _departmentService.Get(id);
            if (response == null) return NotFound();
            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (!ModelState.IsValid) return View(department);
            await _departmentService.Update(id, department);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _departmentService.Get(id);
            if (response == null) return NotFound();
            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _departmentService.Delete(id);
            if (!result.IsSuccess) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
