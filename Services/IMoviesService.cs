using x_sinema.Base;
using x_sinema.ViewModels;
using x_sinema.Models;

namespace x_sinema.Services
{
    public interface IMoviesService : IEntityBaseRepository<MovieModel>
    {
        Task<MovieModel> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownViewModel> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieViewModel data);
        Task UpdateMovieAsync(NewMovieViewModel data);
    }
}