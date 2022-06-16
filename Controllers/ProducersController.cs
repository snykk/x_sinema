using x_sinema.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using x_sinema.Services;
using x_sinema.Models;

namespace x_sinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducersService _producerService;

        public ProducersController(IProducersService producerService)
        {
            _producerService = producerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducersData = await _producerService.GetAllAsync();
            return View(allProducersData);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("ProfilePictureURL,FullName,Bio")] ProducerModel producerModel)
        {
            if (!ModelState.IsValid) return View(producerModel);

            await _producerService.AddAsync(producerModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producerData = await _producerService.GetByIdAsync(id);
            if (producerData == null) return View("NotFound");
            return View(producerData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] ProducerModel producerModel)
        {
            if (!ModelState.IsValid) return View(producerModel);

            await _producerService.UpdateAsync(id, producerModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var producerData = await _producerService.GetByIdAsync(id);
            if (producerData == null) return View("NotFound");
            return View(producerData);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producerData = await _producerService.GetByIdAsync(id);
            if (producerData == null) return View("NotFound");
            return View(producerData);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var producerData = await _producerService.GetByIdAsync(id);
            if (producerData == null) return View("NotFound");

            await _producerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
