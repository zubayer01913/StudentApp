using Microsoft.AspNetCore.Mvc;
using StudentApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace StudentApp
{
    public class ddController : Controller
    {
        private readonly StudentDbContext _dbContext;
        public IEnumerable<StudentViewModel> Students { get; set; }
        public ddController(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
                _dbContext.Students.Remove(student);

            return View();
        }
    }
}
