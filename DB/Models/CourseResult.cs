using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task.DB.Models
{
    [Table("CourseResult")]
    public class CourseResult
    {
        public int Id { get; set; }
        [Range(0,100 , ErrorMessage ="Degree must be between 0 and 100.")]
        public double? Degree { get; set; }
        [Required(ErrorMessage ="Course Id is required")]
        [ForeignKey("Course")]
        public int Crs_Id { get; set; }
        [Required(ErrorMessage = "Trainee Id is required")]
        [ForeignKey("Trainee")]
        public int Trainee_Id { get; set; }
        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}
