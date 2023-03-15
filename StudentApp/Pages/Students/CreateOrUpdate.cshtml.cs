using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StudentApp.Pages.Students
{
    public class CreateOrUpdateModel : PageModel
    {
        [BindProperty]
        public StudentInputModel Student { get; set; } = new StudentInputModel();

        private readonly StudentDbContext _dbContext;
        public CreateOrUpdateModel(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var reslut = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (reslut != null)
                {
                    Student.Id = reslut.Id;
                    Student.Name = reslut.Name;
                    Student.Description = reslut.Description;
                    Student.PhoneNumber = reslut.PhoneNumber;
                    Student.ClassName = reslut.ClassName;
                }
            }

        }

        public async Task OnPost()
        {
            var student = new Student();
            if (Student.Id.HasValue)
                student.Id = Student.Id.Value;

            student.Name = Student.Name;
            student.Description = Student.Description;
            student.PhoneNumber = Student.PhoneNumber;
            student.ClassName = Student.ClassName;

            if (Student.Id.HasValue)
            {
                _dbContext.Students.Update(student);
            }
            else
            {
                _dbContext.Students.Add(student);
            }

          await  _dbContext.SaveChangesAsync();
        }
    }



    public class StudentInputModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string ClassName { get; set; }
    }
}
