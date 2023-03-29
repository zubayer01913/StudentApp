using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;
using StudentApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StudentApp.Pages.Students
{
    public class CreateOrUpdateModel : PageModel
    {
        [BindProperty]
        public StudentInputModel Student { get; set; } = new StudentInputModel();

        public List<SelectListItem> Grades { get; set; }
        public List<SelectListItem> Gender { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        private readonly StudentDbContext _dbContext;
        private IHostingEnvironment _hostEnvironment;
        public CreateOrUpdateModel(StudentDbContext dbContext, IHostingEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
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
                    Student.GradeId = reslut.GradeId;
                    Student.FileName = reslut.FileName;
                    Student.Gender = reslut.Gender;
                }
            }
            Grades = _dbContext.Grades.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            Gender = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(x => new SelectListItem
            {
                Text = GetEnumDescription(x as Enum),
                Value = x.ToString()
            }).ToList();
        }
        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


        public async Task<IActionResult> OnPost()
        {
            var student = new Student();
            if (Student.Id.HasValue)
                student.Id = Student.Id.Value;

            if(Upload != null)
            {
                var file = Path.Combine(_hostEnvironment.WebRootPath, "images", Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                student.FileName = Upload.FileName;
            }
          

            student.Name = Student.Name;
            student.Description = Student.Description;
            student.PhoneNumber = Student.PhoneNumber;
            student.ClassName = Student.ClassName;
            student.GradeId = Student.GradeId;
            student.Gender = Student.Gender;


            if (Student.Id.HasValue)
            {
                _dbContext.Students.Update(student);
            }
            else
            {
                _dbContext.Students.Add(student);
            }

            await _dbContext.SaveChangesAsync();

            return Redirect("./Index");
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
        public int? GradeId { get; set; }
        public string Gender { get; set; }
        public string FileName { get; set; }
    }
}
