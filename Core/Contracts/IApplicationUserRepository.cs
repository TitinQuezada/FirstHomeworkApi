using Core.Models;
using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IApplicationUserRepository: IBaseRepository<ApplicationUser>
    {
        /// <summary>
        /// Gets the list of all users registered. 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApplicationUser> GetUsers();
    }
}
