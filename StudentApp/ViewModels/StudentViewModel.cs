using System.ComponentModel.DataAnnotations;

namespace StudentApp.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string ClassName { get; set; }
    }
}
