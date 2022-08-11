using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Models
{
    public class StudentCreation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public List<int> courses { get; set; }
    }
}
