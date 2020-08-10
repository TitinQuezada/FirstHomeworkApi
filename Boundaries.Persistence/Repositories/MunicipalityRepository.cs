using Core.Contracts;
using Core.Models;

namespace Boundaries.Persistence.Repositories
{
    public sealed class MunicipalityRepository : BaseRepository<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(FristHomeworkDbContext context) : base(context)
        {
        }
    }
}
