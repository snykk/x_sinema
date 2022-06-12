using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using x_sinema.Constans;
using x_sinema.Models;
using x_sinema.Services;

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
            var allMovieData = await _movieService.GetAllAsync(n => n.Cinema!);
            return View(allMovieData);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}