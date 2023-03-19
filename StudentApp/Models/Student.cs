using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string ClassName { get; set; }


        public int? StudentInfoId { get; set; }
        public StudentInfo StudentInfo { get; set; }

        public int? GradeId { get; set; }
        public Grade Grade { get; set; }

    }
}
