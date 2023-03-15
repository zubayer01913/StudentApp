using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;
using StudentApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentDbContext _dbContext;

        public IEnumerable<StudentViewModel> Students { get; set; }
        public IndexModel(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            var output = await _dbContext.Students.ToListAsync();
            Students = output.Select(x => new StudentViewModel { ClassName = x.ClassName, Id = x.Id, Description = x.Description, Name = x.Name });
        }
    }
}
