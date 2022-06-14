using x_sinema.Base;
using x_sinema.Data;
using x_sinema.Models;

namespace x_sinema.Services
{
    public class ActorsService : EntityBaseRepository<ActorModel>, IActorsService
    {
        public ActorsService(ApplicationDbContext _db) : base(_db) { }
    }
}
