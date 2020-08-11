using Core.Contracts;
using Core.Models;

namespace Boundaries.Persistence.Repositories
{
    public sealed class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly FristHomeworkDbContext _context;
        public ApplicationUserRepository(FristHomeworkDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
