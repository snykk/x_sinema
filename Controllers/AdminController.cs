using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using x_sinema.Constans;
using x_sinema.Data;

namespace x_sinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> ListOfUsers()
        {
            var listOfUsers = await _db.Users.ToListAsync();
            return View(listOfUsers);
        }
    }
}
