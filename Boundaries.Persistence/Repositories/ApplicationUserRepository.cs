using Core.Contracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Boundaries.Persistence.Repositories
{
    public sealed class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly FristHomeworkDbContext _context;
        public ApplicationUserRepository(FristHomeworkDbContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            IEnumerable<ApplicationUser> users = _context.ApplicationUsers.Include("Phone")
                                                                          .Include("Address")
                                                                          .Include("Role").ToList();

            return users;
        }
    }
}
