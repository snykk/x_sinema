using x_sinema.Base;
using x_sinema.Data;
using x_sinema.Models;

namespace x_sinema.Services
{
    public class ProducersService : EntityBaseRepository<ProducerModel>, IProducersService
    {
        public ProducersService(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}