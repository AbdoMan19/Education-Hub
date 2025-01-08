namespace MVC_Task.Services.ViewModels.TraineeVM
{
    public class TraineeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image {  get; set; }
        public string? Department { get; set; }
        public double? Grade { get; set; }
        public List<TraineeCourseViewModel> Courses { get; set; } =new List<TraineeCourseViewModel>();
    }
}
