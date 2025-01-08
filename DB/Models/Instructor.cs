using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task.DB.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50 characters.")]
        public string Name { get; set; }

        [Url(ErrorMessage = "Invalid URL format for Image")]
        public string? Image { get; set; }

        [Range(0, 50000, ErrorMessage = "Salary must be between 0 and 50000.")]
        public double? Salary { get; set; }

        [StringLength(100, ErrorMessage = "Address length can't be more than 100 characters.")]
        public string Address { get; set; }

        [ForeignKey("Department")]
        [Column("Dept_Id")]
        public int? DepartmentId { get; set; }

        [ForeignKey("Course")]
        [Column("Crs_Id")]
        public int? CourseId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Course? Course { get; set; }
    }
}