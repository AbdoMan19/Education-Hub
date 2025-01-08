using MVC_Task.Common;
using MVC_Task.DB.Models;
using MVC_Task.Services.ViewModels.TraineeVM;

namespace MVC_Task.Services.TraineeService
{
    public interface ITraineeService
    {
        public Task<GenericResponseModel<TraineeViewModel>> Get(int id);
        public Task<GenericResponseModel<Trainee>> GetById(int id);

        public Task<GenericResponseModel<bool>> Delete(int id);
        public Task<GenericResponseModel<Trainee>> Update(int id , Trainee traineeViewModel);
        public Task<GenericResponseModel<IEnumerable<Trainee>>> GetAll();
        public Task<GenericResponseModel<Trainee>> Add(Trainee trainee);
    }
}
