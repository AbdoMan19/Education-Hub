using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.Services.ViewModels.TraineeVM;
using MVC_Task.UnitOfWork;
using System.ComponentModel.DataAnnotations;
using static MVC_Task.Common.BaseResponseModel;


namespace MVC_Task.Services.TraineeService
{
    public class TraineeService : ITraineeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TraineeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<Trainee>> Add(Trainee trainee)
        {
            var newTrainee = _unitOfWork.Repository<Trainee>().Add(trainee);
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<Trainee>.Success(newTrainee);

        }

        public async Task<GenericResponseModel<bool>> Delete(int id)
        {
            var trainee = await _unitOfWork.Repository<Trainee>().Delete(id);
            if (trainee == null)
            {
                return GenericResponseModel<bool>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Trainee Not Found") });
            }

            await _unitOfWork.SaveChanges();
            return GenericResponseModel<bool>.Success(true);

        }
        public async Task<GenericResponseModel<TraineeViewModel>> Get(int id)
        {
            var trainee = await _unitOfWork.Repository<Trainee>().FindBy(t => t.Id == id).Include(t => t.Department).FirstOrDefaultAsync();
            if (trainee == null)
            {
                return GenericResponseModel<TraineeViewModel>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Trainee Not Found") });

            }
            var coursesResult = await _unitOfWork.Repository<CourseResult>()
            .FindBy(cr => cr.Trainee_Id == id)
            .Include(cr => cr.Course)
            .ToListAsync();

            var courseViewModels = new List<TraineeCourseViewModel>();

            foreach (var course in coursesResult)
            {
                courseViewModels.Add(new TraineeCourseViewModel { Name = course.Course.Name, Grade = course.Degree, MinDegree = course.Course.MinDegree });
            };

            var traineeViewModel = new TraineeViewModel
            {
                Id = trainee.Id,
                Name = trainee.Name,
                Department = trainee.Department?.Name,
                Grade = trainee.Grade,
                Image = trainee.Image,
                Courses = courseViewModels
            };
            return GenericResponseModel<TraineeViewModel>.Success(traineeViewModel);
        }
        public async Task<GenericResponseModel<IEnumerable<Trainee>>> GetAll()
        {
            var trainees = await _unitOfWork.Repository<Trainee>().GetAll(["Department"]);
            return GenericResponseModel<IEnumerable<Trainee>>.Success(trainees.ToList());
        }

        public async Task<GenericResponseModel<Trainee>> GetById(int id)
        {
            var trainee = await _unitOfWork.Repository<Trainee>().FindBy(t => t.Id == id).Include(t => t.Department).FirstOrDefaultAsync();
            if (trainee == null)
            {
                return GenericResponseModel<Trainee>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Trainee Not Found") });
            }
            return GenericResponseModel<Trainee>.Success(trainee);
        }

        public async Task<GenericResponseModel<Trainee>> Update(int id , Trainee trainee)
        {
            var existingTrainee = await _unitOfWork.Repository<Trainee>().GetById(id);

            if (existingTrainee == null)
            {
                return GenericResponseModel<Trainee>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Trainee Not Found") });
            }
            
            existingTrainee.Name = trainee.Name;
            existingTrainee.Image = trainee.Image;
            existingTrainee.Grade = trainee.Grade;
            existingTrainee.DepartmentId = trainee.DepartmentId;

            var updatedTrainee = await _unitOfWork.Repository<Trainee>().Update(existingTrainee);
            await _unitOfWork.SaveChanges();

            return GenericResponseModel<Trainee>.Success(updatedTrainee);
        
        }

        
    }
}
