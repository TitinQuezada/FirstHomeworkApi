using Core.Contracts;
using Core.Models;

namespace Boundaries.Persistence.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(FristHomeworkDbContext context) : base(context)
        {
        }
    }
}
