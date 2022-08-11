using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Hours { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public virtual ICollection<StudentCourse>? StudentCourses { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department? Dept { get; set; }
    }
}
