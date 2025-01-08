using Microsoft.AspNetCore.Mvc;
using MVC_Task.Services.CourseResultService;
using MVC_Task.Services.ViewModels.CourseResultVM;

namespace MVC_Task.Controllers
{
    public class CourseResultController : Controller
    {
        private readonly ICourseResultService _courseResultService;

        public CourseResultController(ICourseResultService courseResultService)
        {
            _courseResultService = courseResultService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _courseResultService.GetAll();
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _courseResultService.Get(id);
            if (!response.IsSuccess)
            {
                return NotFound();
            }
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseResultVMToAdd courseResultVMToAdd)
        {
            if (ModelState.IsValid)
            {
                var response = await _courseResultService.Add(courseResultVMToAdd);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in response.ErrorList)
                {
                    ModelState.AddModelError(error.PropertyName, error.Message);
                }
            }
            return View(courseResultVMToAdd);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _courseResultService.Get(id);
            if (!response.IsSuccess)
            {
                return NotFound();
            }
            var courseResultVmToAdd = new CourseResultVMToAdd
            {
                Degree = response.Data.Degree,
                Trainee_Id = response.Data.Trainee_Id,
                Crs_Id = response.Data.Crs_Id
            };
            return View(courseResultVmToAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseResultVMToAdd courseResultViewModelToAdd)
        {
            if (ModelState.IsValid)
            {
                var response = await _courseResultService.Update(id, courseResultViewModelToAdd);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in response.ErrorList)
                {
                    ModelState.AddModelError(error.PropertyName, error.Message);
                }
            }
            return View(courseResultViewModelToAdd);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _courseResultService.Get(id);
            if (!response.IsSuccess)
            {
                return NotFound();
            }
            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _courseResultService.Delete(id);
            if (!response.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
