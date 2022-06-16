using x_sinema.Base;
using System.ComponentModel.DataAnnotations;

namespace x_sinema.Models
{
    public class CompanyModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema logo is required")]
        public string? Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        public string? Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema description is required")]
        public string? Description { get; set; }

        //Relationships
        public List<MovieModel>? Movies { get; set; }
    }
}
