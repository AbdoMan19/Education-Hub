using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.UnitOfWork;

namespace MVC_Task.Services.InstructorService
{
    public interface IInstructorService
    {
        public Task<GenericResponseModel<Instructor>> Get(int id);
        public Task<GenericResponseModel<bool>> Delete(int id);
        public Task<GenericResponseModel<Instructor>> Update(int id, Instructor instructor);
        public Task<GenericResponseModel<IEnumerable<Instructor>>> GetAll();
        public Task<GenericResponseModel<Instructor>> Add(Instructor instructor);
    }
}
