using Microsoft.EntityFrameworkCore;
using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.UnitOfWork;
using static MVC_Task.Common.BaseResponseModel;

namespace MVC_Task.Services.CourseService
{
    public class CourseService :ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<Course>> Add(Course course)
        {
            var newCourse = _unitOfWork.Repository<Course>().Add(course);
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<Course>.Success(newCourse);
        }

        public async Task<GenericResponseModel<bool>> Delete(int id)
        {
            var course = await _unitOfWork.Repository<Course>().Delete(id);
            if (course is null)
            {
                return GenericResponseModel<bool>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Course Not Found") });
            }
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<bool>.Success(true);
        }

        public async Task<GenericResponseModel<Course>> Get(int id)
        {
            var course = await _unitOfWork.Repository<Course>()
                .FindBy(d => d.Id == id)
                .Include(c => c.Department)
                .Include(c => c.Instructors)
                .FirstOrDefaultAsync();
            if (course is null)
            {
                return GenericResponseModel<Course>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Course Not Found") });
            }
            return GenericResponseModel<Course>.Success(course);
        }

        public async Task<GenericResponseModel<IEnumerable<Course>>> GetAll()
        {
            var courses = await _unitOfWork.Repository<Course>().GetAll(["Department" , "Instructors"]);
            return GenericResponseModel<IEnumerable<Course>>.Success(courses.ToList());
        }

        public async Task<GenericResponseModel<Course>> Update(int id, Course course)
        {
            var existingCourse = await _unitOfWork.Repository<Course>().GetById(id);

            if (existingCourse == null)
            {
                return GenericResponseModel<Course>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Course Not Found") });
            }

            existingCourse.Name = course.Name;
            existingCourse.Degree = course.Degree;
            existingCourse.MinDegree = course.MinDegree;
            existingCourse.DepartmentId = course.DepartmentId;

            var updatedCourse = await _unitOfWork.Repository<Course>().Update(existingCourse);
            await _unitOfWork.SaveChanges();

            return GenericResponseModel<Course>.Success(existingCourse);
        }
    }
}
