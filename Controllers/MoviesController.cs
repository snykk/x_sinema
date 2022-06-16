using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using x_sinema.Constans;
using x_sinema.Models;
using x_sinema.Services;
using x_sinema.ViewModels;

namespace x_sinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesService _movieService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovieData = await _movieService.GetAllAsync(n => n.Company!);
            return View(allMovieData);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Companies, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Companies, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(model);
            }

            await _movieService.AddNewMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var movieDetail = await _movieService.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movieData = await _movieService.GetMovieByIdAsync(id);
            if (movieData == null) return View("NotFound");

            var response = new NewMovieViewModel()
            {
                Id = movieData.Id,
                Name = movieData.Name,
                Description = movieData.Description,
                Price = movieData.Price,
                StartDate = movieData.StartDate,
                EndDate = movieData.EndDate,
                ImageURL = movieData.ImageURL,
                MovieCategory = movieData.MovieCategory,
                CompanyId = movieData.CompanyId,
                ProducerId = movieData.ProducerId,
                ActorIds = movieData.MovieActor!.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Companies, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieViewModel movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Companies, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _movieService.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}