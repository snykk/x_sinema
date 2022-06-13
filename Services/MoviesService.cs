using x_sinema.Base;
using Microsoft.EntityFrameworkCore;
using x_sinema.Data;
using x_sinema.Models;
using x_sinema.ViewModels;

namespace x_sinema.Services
{
    public class MoviesService : EntityBaseRepository<MovieModel>, IMoviesService
    {
        private readonly ApplicationDbContext _db;
        public MoviesService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddNewMovieAsync(NewMovieViewModel data)
        {
            var newMovie = new MovieModel()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new MovieActorModel()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _db.Actors_Movies.AddAsync(newActorMovie);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<MovieModel> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _db.Movies
                .Include(c => c.Cinema!)
                .Include(p => p.Producer!)
                .Include(am => am.MovieActor!).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails!;
        }

        public async Task<NewMovieDropdownViewModel> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownViewModel()
            {
                Actors = await _db.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _db.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _db.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieViewModel data)
        {
            var dbMovie = await _db.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _db.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _db.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _db.Actors_Movies.RemoveRange(existingActorsDb);
            await _db.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new MovieActorModel()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _db.Actors_Movies.AddAsync(newActorMovie);
            }
            await _db.SaveChangesAsync();
        }
    }
}
