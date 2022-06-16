using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using x_sinema.Base;
using x_sinema.Constans;

namespace x_sinema.Models
{
    public class MovieModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationship
        public List<MovieActorModel>? MovieActor { get; set; }

        //Company
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public CompanyModel? Company { get; set; }

        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public ProducerModel Producer { get; set; }
    }
}
