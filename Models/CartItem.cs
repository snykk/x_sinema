using System.ComponentModel.DataAnnotations;

namespace x_sinema.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public MovieModel Movie { get; set; }
        public int Amount { get; set; }


        public string CartId { get; set; }
    }
}
