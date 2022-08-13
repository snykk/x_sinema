using x_sinema.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using x_sinema.Services;
using x_sinema.Models;

namespace x_sinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCompaniesData = await _companiesService.GetAllAsync();
            return View(allCompaniesData);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var companyData = await _companiesService.GetByIdAsync(id);
            if (companyData == null) return View("NotFound");
            return View(companyData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] CompanyModel companyModel)
        {
            if (!ModelState.IsValid) return View(companyModel);
            await _companiesService.UpdateAsync(id, companyModel);
            TempData["message"] = MyFlasher.flasher("Company data update successfully", berhasil: true);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var companyData = await _companiesService.GetByIdAsync(id);
            if (companyData == null) return View("NotFound");
            return View(companyData);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Logo,Name,Description")] CompanyModel companyModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = MyFlasher.flasher("Oops error was found", gagal: true);

                return View(companyModel);
            }

            await _companiesService.AddAsync(companyModel);
            TempData["message"] = MyFlasher.flasher("Company data <strong>updated</strong> successfully", berhasil: true);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var companyData = await _companiesService.GetByIdAsync(id);
            if (companyData == null) return View("NotFound");
            return View(companyData);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var companyData = await _companiesService.GetByIdAsync(id);
            if (companyData == null) return View("NotFound");

            await _companiesService.DeleteAsync(id);
            TempData["message"] = MyFlasher.flasher("Company data <strong>deleted</strong> successfully", berhasil: true);

            return RedirectToAction(nameof(Index));
        }
    }
}
