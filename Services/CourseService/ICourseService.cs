using MVC_Task.Common;
using MVC_Task.DB.Models;

namespace MVC_Task.Services.CourseService
{
    public interface ICourseService
    {
        public Task<GenericResponseModel<bool>> Delete(int id);
        public Task<GenericResponseModel<Course>> Update(int id, Course course);
        public Task<GenericResponseModel<IEnumerable<Course>>> GetAll();
        public Task<GenericResponseModel<Course>> Add(Course course);
        public Task<GenericResponseModel<Course>> Get(int id);
    }
}
