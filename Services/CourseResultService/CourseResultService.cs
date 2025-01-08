using Microsoft.EntityFrameworkCore;
using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.Services.ViewModels.CourseResultVM;
using MVC_Task.UnitOfWork;
using static MVC_Task.Common.BaseResponseModel;

namespace MVC_Task.Services.CourseResultService
{
    public class CourseResultService : ICourseResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<CourseResult>> Add(CourseResultVMToAdd courseResultVMToAdd)
        {
            var trainee = await _unitOfWork.Repository<Trainee>().GetById(courseResultVMToAdd.Trainee_Id);
            if (trainee == null) return GenericResponseModel<CourseResult>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(courseResultVMToAdd.Trainee_Id), "Trainee Not Found") });
            var course = await _unitOfWork.Repository<Course>().GetById(courseResultVMToAdd.Crs_Id);
            if (course == null) return GenericResponseModel<CourseResult>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(courseResultVMToAdd.Crs_Id), "Course Not Found") });
            var CourseResult = new CourseResult
            {
                Degree = courseResultVMToAdd.Degree,
                Crs_Id = courseResultVMToAdd.Crs_Id,
                Trainee_Id = courseResultVMToAdd.Trainee_Id
            };
            var newCourseResult = _unitOfWork.Repository<CourseResult>().Add(CourseResult);
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<CourseResult>.Success(newCourseResult);
        }

        public async Task<GenericResponseModel<bool>> Delete(int id)
        {
            var courseResult = await _unitOfWork.Repository<CourseResult>().Delete(id);
            if (courseResult is null)
            {
                return GenericResponseModel<bool>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "CourseResult Not Found") });
            }
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<bool>.Success(true);
        }

        public async Task<GenericResponseModel<CourseResult>> Get(int id)
        {
            var courseResult = await _unitOfWork.Repository<CourseResult>()
                .FindBy(cr => cr.Id == id)
                .Include(cr => cr.Trainee)
                .Include(cr => cr.Course)
                .FirstOrDefaultAsync();
            if (courseResult is null)
            {
                return GenericResponseModel<CourseResult>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "CourseResult Not Found") });
            }
            return GenericResponseModel<CourseResult>.Success(courseResult);
        }

        public async Task<GenericResponseModel<IEnumerable<CourseResult>>> GetAll()
        {
            var courseResults = await _unitOfWork.Repository<CourseResult>().GetAll(["Trainee" , "Course"]);
            return GenericResponseModel<IEnumerable<CourseResult>>.Success(courseResults.ToList());
        }

        public async Task<GenericResponseModel<CourseResult>> Update(int id, CourseResultVMToAdd courseResultViewModelToAdd)
        {
            var existingCourseResult = await _unitOfWork.Repository<CourseResult>().GetById(id);

            if (existingCourseResult == null)
            {
                return GenericResponseModel<CourseResult>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "CourseResult Not Found") });
            }
            var trainee = await _unitOfWork.Repository<Trainee>().GetById(courseResultViewModelToAdd.Trainee_Id);
            if (trainee == null) return GenericResponseModel<CourseResult>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(courseResultViewModelToAdd.Trainee_Id), "Trainee Not Found") });
            var course = await _unitOfWork.Repository<Course>().GetById(courseResultViewModelToAdd.Crs_Id);
            if (course == null) return GenericResponseModel<CourseResult>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(courseResultViewModelToAdd.Crs_Id), "Course Not Found") });
            existingCourseResult.Degree = courseResultViewModelToAdd.Degree;
            existingCourseResult.Trainee_Id = courseResultViewModelToAdd.Trainee_Id;
            existingCourseResult.Crs_Id = courseResultViewModelToAdd.Crs_Id;

            var updatedCourseResult = await _unitOfWork.Repository<CourseResult>().Update(existingCourseResult);
            await _unitOfWork.SaveChanges();

            return GenericResponseModel<CourseResult>.Success(updatedCourseResult);
        }


    }
}
