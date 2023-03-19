using System.Collections;
using System.Collections.Generic;

namespace StudentApp.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }  
    }
}
