using Core.Contracts;
using Core.Models;

namespace Boundaries.Persistence.Repositories
{
    public sealed class UserAddressRepository : BaseRepository<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(FristHomeworkDbContext context) : base(context)
        {
        }
    }
}
