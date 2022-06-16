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
            Companies = new List<CompanyModel>();
            Actors = new List<ActorModel>();
        }

        public List<ProducerModel> Producers { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public List<ActorModel> Actors { get; set; }
    }
}
