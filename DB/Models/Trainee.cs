using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task.DB.Models
{
    [Table("Trainee")]
    public class Trainee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }

        [Url(ErrorMessage = "Invalid URL format for Image")]
        public string? Image { get; set; }

        [Range(0, 4, ErrorMessage = "Grade must be between 0 and 4.")]
        public double? Grade { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }

    }
}
