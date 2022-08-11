namespace EducationalPlatform.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
