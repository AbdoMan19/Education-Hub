using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Task.DB.Models;
using MVC_Task.Services.DepartmentService;
using MVC_Task.Services.TraineeService;


namespace MVC_Task.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ITraineeService _traineeService;
        private readonly IDepartmentService _departmentService;

        public TraineeController(ITraineeService traineeService , IDepartmentService departmentService)
        {
            _traineeService = traineeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _traineeService.GetAll();
            return View(response.Data); 
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _traineeService.Get(id);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }

            return NotFound();
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");

            return View();
        }

        // POST: Trainee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                var response = await _traineeService.Add(trainee);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");

            return View(trainee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _traineeService.GetById(id);
            if (response.IsSuccess)
            {
                var departments = await _departmentService.GetAll();
                ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
                return View(response.Data);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                var response = await _traineeService.Update(id, trainee);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var departments = await _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments.Data.ToList(), "Id", "Name");
            return View(trainee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _traineeService.GetById(id);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _traineeService.Delete(id);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound(); 
        }
    }
}
