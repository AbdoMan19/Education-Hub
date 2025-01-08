using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.Services.ViewModels.CourseResultVM;

namespace MVC_Task.Services.CourseResultService
{
    public interface ICourseResultService
    {
        public Task<GenericResponseModel<bool>> Delete(int id);
        public Task<GenericResponseModel<CourseResult>> Update(int id, CourseResultVMToAdd courseResultViewModelToAdd);
        public Task<GenericResponseModel<IEnumerable<CourseResult>>> GetAll();
        public Task<GenericResponseModel<CourseResult>> Add(CourseResultVMToAdd courseResultViewModelToAdd);
        public Task<GenericResponseModel<CourseResult>> Get(int id);

    }
}
