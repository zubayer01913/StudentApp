using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentApp.Models;
using StudentApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly StudentDbContext _dbContext;

        [BindProperty]
        public Student StudentData { get; set; }
        public DeleteModel(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            var data = _dbContext.Students.FirstOrDefault(x => x.Id == id);
            StudentData = data;
        }

        public async Task<IActionResult> OnPost()
        {
            _dbContext.Students.Remove(StudentData);
            await _dbContext.SaveChangesAsync();
            return Redirect("./Index");
        }
    }
}
