using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public int Age { get; set; }

        public string Course { get; set; }

        public string Email { get; set; }
    }
}