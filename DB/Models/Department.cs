using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task.DB.Models
{
    [Table("Department")]
    public class Department
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false ,ErrorMessage ="Name is required")]
        [StringLength(50 , ErrorMessage = "Name length can't be more than 50 characters.")]
        [Unique<Department>("Name", ErrorMessage ="Name must be unique")]
        public string Name { get; set; }
        public string? Manager { get; set; }

        public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();
        public virtual ICollection<Instructor>? Instructors { get; set; } = new List<Instructor>();
        public virtual ICollection<Trainee>? Trainees { get; set; } = new List<Trainee>();
    }
}
