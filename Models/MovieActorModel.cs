namespace x_sinema.Models
{
    public class MovieActorModel
    {
        public int MovieId { get; set; }
        public MovieModel? Movie { get; set; }

        public int ActorId { get; set; }
        public ActorModel? Actor { get; set; }
    }
}
