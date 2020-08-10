using Core.Contracts;
using Core.Models;

namespace Boundaries.Persistence.Repositories
{
    public sealed class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(FristHomeworkDbContext context) : base(context)
        {
        }
    }
}
