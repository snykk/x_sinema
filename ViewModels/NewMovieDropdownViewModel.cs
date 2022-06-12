using x_sinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace x_sinema.ViewModels
{
    public class NewMovieDropdownViewModel
    {
        public NewMovieDropdownViewModel()
        {
            Producers = new List<ProducerModel>();
            Cinemas = new List<CinemaModel>();
            Actors = new List<ActorModel>();
        }

        public List<ProducerModel> Producers { get; set; }
        public List<CinemaModel> Cinemas { get; set; }
        public List<ActorModel> Actors { get; set; }
    }
}
