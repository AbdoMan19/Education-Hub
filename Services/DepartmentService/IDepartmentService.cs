using MVC_Task.Common;
using MVC_Task.DB.Models;
using System.Linq.Expressions;

namespace MVC_Task.Services.DepartmentService
{
    public interface IDepartmentService
    {
        public Task<GenericResponseModel<bool>> Delete(int id);
        public Task<GenericResponseModel<Department>> Update(int id, Department department);
        public Task<GenericResponseModel<IEnumerable<Department>>> GetAll();
        public Task<GenericResponseModel<Department>> Add(Department department);
        public Task<GenericResponseModel<Department>> Get(int id);


    }
}
