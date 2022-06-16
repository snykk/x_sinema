using x_sinema.Base;
using x_sinema.Data;
using x_sinema.Models;

namespace x_sinema.Services
{
    public class CompaniesService : EntityBaseRepository<CompanyModel>, ICompaniesService
    {
        public CompaniesService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
