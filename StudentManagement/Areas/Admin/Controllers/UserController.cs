using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Contexts;

namespace StudentManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Users.ToListAsync();
            return View(data);
        }

        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Remove(string id)
        {
            var selectedUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
