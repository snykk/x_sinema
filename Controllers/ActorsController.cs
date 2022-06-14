using x_sinema.Constans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using x_sinema.Services;
using x_sinema.Models;

namespace x_sinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorService;

        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }

        // mengambil data actor by id
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
    }
}
