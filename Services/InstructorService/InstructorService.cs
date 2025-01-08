using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.UnitOfWork;
using static MVC_Task.Common.BaseResponseModel;

namespace MVC_Task.Services.InstructorService
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<Instructor>> Add(Instructor instructor)
        {
            var newInstructor = _unitOfWork.Repository<Instructor>().Add(instructor);
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<Instructor>.Success(newInstructor);

        }

        public async Task<GenericResponseModel<bool>> Delete(int id)
        {
            var instructor = await _unitOfWork.Repository<Instructor>().Delete(id);
            if (instructor is null) {
                return GenericResponseModel<bool>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Instructor Not Found") });
            }
            await _unitOfWork.SaveChanges();
            return GenericResponseModel<bool>.Success(true);
        }

        public async Task<GenericResponseModel<Instructor>> Get(int id)
        {
            var instructor = await _unitOfWork.Repository<Instructor>()
            .FindBy(d => d.Id == id)
            .Include(i => i.Course)
            .Include(i => i.Department)
            .FirstOrDefaultAsync();
            if (instructor is null)
            {
                return GenericResponseModel<Instructor>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Department Not Found") });
            }
            return GenericResponseModel<Instructor>.Success(instructor);
        }

        public async Task<GenericResponseModel<IEnumerable<Instructor>>> GetAll()
        {
            var instructors = await _unitOfWork.Repository<Instructor>().GetAll(["Department" , "Course"]);
            return GenericResponseModel<IEnumerable<Instructor>>.Success(instructors.ToList());
        }

        public async Task<GenericResponseModel<Instructor>> Update(int id, Instructor instructor)
        {
            var existingInstructor = await _unitOfWork.Repository<Instructor>().GetById(id);

            if (existingInstructor == null)
            {
                return GenericResponseModel<Instructor>.Failure(new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Instructor Not Found") });
            }

            existingInstructor.Name = instructor.Name;
            existingInstructor.Image = instructor.Image;
            existingInstructor.Salary = instructor.Salary;
            existingInstructor.Address = instructor.Address;
            existingInstructor.DepartmentId = instructor.DepartmentId;
            existingInstructor.CourseId = instructor.CourseId;

            var updatedInstructor = await _unitOfWork.Repository<Instructor>().Update(existingInstructor);
            await _unitOfWork.SaveChanges();

            return GenericResponseModel<Instructor>.Success(updatedInstructor);
        }

    }
}
