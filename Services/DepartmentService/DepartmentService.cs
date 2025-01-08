using Microsoft.EntityFrameworkCore;
using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.UnitOfWork;
using static MVC_Task.Common.BaseResponseModel;

namespace MVC_Task.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<Department>> Add(Department department)
        {
            var newDepartment = _unitOfWork.Repository<Department>().Add(department);
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<Department>.Success(newDepartment);
            
        }

        public async Task<GenericResponseModel<bool>> Delete(int id)
        {
            var department = await _unitOfWork.Repository<Department>().Delete(id);
            if (department is null)
            {
                return GenericResponseModel<bool>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Department Not Found") });
            }
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<bool>.Success(true);
        }

        public async Task<GenericResponseModel<Department>> Get(int id)
        {
            var department = await _unitOfWork.Repository<Department>()
                .FindBy(d => d.Id == id)
                .Include(d => d.Courses)
                .Include(d => d.Instructors)
                .Include(d => d.Trainees)
                .FirstOrDefaultAsync();
            if(department is null)
            {
                return GenericResponseModel<Department>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Department Not Found") });
            }
            return GenericResponseModel<Department>.Success(department);
        }

        public async Task<GenericResponseModel<IEnumerable<Department>>> GetAll()
        {
            var departments = await _unitOfWork.Repository<Department>().GetAll(["Trainees", "Courses" , "Instructors"]);
            return GenericResponseModel<IEnumerable<Department>>.Success(departments.ToList());
        }

        public async Task<GenericResponseModel<Department>> Update(int id, Department department)
        {
            var existingDepartment = await _unitOfWork.Repository<Department>().GetById(id);

            if (existingDepartment == null)
            {
                return GenericResponseModel<Department>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "User Not Found") });
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Manager = department.Manager;
            UpdateCourses(existingDepartment, department);
            UpdateInstructors(existingDepartment, department);
            UpdateTrainees(existingDepartment, department);

            var updatedDepartment = await _unitOfWork.Repository<Department>().Update(existingDepartment);
            await _unitOfWork.SaveChanges();

            return GenericResponseModel<Department>.Success(updatedDepartment);
        }
        private async void UpdateCourses(Department existingDepartment , Department updatedDepartment)
        {
            var courseIds = updatedDepartment.Courses?.Select(c => c.Id).ToList() ?? new List<int>();
            var exsistingCourseIds = existingDepartment.Courses?.Select(c => c.Id).ToList() ?? new List<int>();

            var courseToRemove = existingDepartment.Courses?.Where(oldIds => !courseIds.Contains(oldIds.Id)).ToList() ?? new List<Course>();
            var courseIdsToAdd = courseIds?.Where(id => !exsistingCourseIds.Contains(id)).ToList() ?? new List<int>();
            var coursesToAdd = await _unitOfWork.Repository<Course>().GetData(c => courseIdsToAdd.Contains(c.Id));
            foreach (var course in courseToRemove)
            {
                existingDepartment.Courses.Remove(course);

            }
            foreach (var course in coursesToAdd)
            {
                existingDepartment.Courses.Add(course);
            }

        }
        private async void UpdateInstructors(Department existingDepartment, Department updatedDepartment)
        {
            var instructorIds = updatedDepartment.Instructors?.Select(c => c.Id).ToList() ?? new List<int>();
            var exsistinginstructorIds = existingDepartment.Instructors?.Select(c => c.Id).ToList() ?? new List<int>();

            var instructorToRemove = existingDepartment.Instructors?.Where(oldIds => !instructorIds.Contains(oldIds.Id)).ToList() ?? new List<Instructor>();
            var instructorIdsToAdd = instructorIds?.Where(id => !exsistinginstructorIds.Contains(id)).ToList() ?? new List<int>();
            var instructorToAdd = await _unitOfWork.Repository<Instructor>().GetData(c => instructorIdsToAdd.Contains(c.Id));
            foreach (var instructor in instructorToRemove)
            {
                existingDepartment.Instructors.Remove(instructor);

            }
            foreach (var instructor in instructorToAdd)
            {
                existingDepartment.Instructors.Add(instructor);
            }

        }
        private async void UpdateTrainees(Department existingDepartment, Department updatedDepartment)
        {
            var traineeIds = updatedDepartment.Trainees?.Select(c => c.Id).ToList() ?? new List<int>();
            var exsistingtraineeIds = existingDepartment.Trainees?.Select(c => c.Id).ToList() ?? new List<int>();

            var traineeToRemove = existingDepartment.Trainees?.Where(oldIds => !traineeIds.Contains(oldIds.Id)).ToList() ?? new List<Trainee>();
            var traineeIdsToAdd = traineeIds?.Where(id => !exsistingtraineeIds.Contains(id)).ToList() ?? new List<int>();
            var traineeToAdd = await _unitOfWork.Repository<Trainee>().GetData(c => traineeIdsToAdd.Contains(c.Id));
            foreach (var trainee in traineeToRemove)
            {
                existingDepartment.Trainees.Remove(trainee);

            }
            foreach (var trainee in traineeToAdd)
            {
                existingDepartment.Trainees.Add(trainee);
            }

        }
        
    }
}
