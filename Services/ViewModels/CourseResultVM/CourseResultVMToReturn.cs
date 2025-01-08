using MVC_Task.DB.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task.Services.ViewModels.CourseResultVM
{
    public class CourseResultVMToReturn
    {
        public double? Degree { get; set; }
        [Required(ErrorMessage = "Course Id is required")]
        [ForeignKey("Course")]
        public int Crs_Id { get; set; }
        [Required(ErrorMessage = "Trainee Id is required")]
        [ForeignKey("Trainee")]
        public int Trainee_Id { get; set; }
        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}
