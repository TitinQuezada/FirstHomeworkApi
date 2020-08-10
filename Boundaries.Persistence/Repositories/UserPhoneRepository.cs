using Core.Contracts;
using Core.Models;

namespace Boundaries.Persistence.Repositories
{
    public sealed class UserPhoneRepository : BaseRepository<UserPhone> , IUserPhoneRepository
    {
        public UserPhoneRepository(FristHomeworkDbContext context) : base(context)
        {
        }
    }
}
