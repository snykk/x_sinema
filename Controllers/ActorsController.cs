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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var actorData = await _actorService.GetByIdAsync(id);

            if (actorData == null) return View("NotFound");
            return View(actorData);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allActorData = await _actorService.GetAllAsync();
            return View(allActorData);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("FullName,ProfilePictureURL,Bio")] ActorModel actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actorData = await _actorService.GetByIdAsync(id);
            if (actorData == null) return View("NotFound");
            return View(actorData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] ActorModel actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }


    }
}
