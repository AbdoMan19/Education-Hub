using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task.DB.Models
{
    [Table("Course")]
    public class Course
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50 characters.")]
        [Unique<Course>("Name" , ErrorMessage ="Name must be unique")]
        public string Name { get; set; }
        public double? Degree { get; set; }
        [Required(ErrorMessage = "Minimum Degree is required")]
        public double MinDegree { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public IEnumerable<Instructor>? Instructors { get; set; } = new List<Instructor>();
    }
}
