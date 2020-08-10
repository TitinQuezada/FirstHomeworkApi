using Core.Contracts;
using Core.Managers;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FirstHomeworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationUserManager _applicationUserManager;
        public UserController(ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ApplicationUserCreateViewModel user)
        {
            IOperationResult<bool> createResult = await _applicationUserManager.Create(user);
            if (!createResult.Success)
            {
                return BadRequest(createResult.Message);
            }

            return Ok();
        }
    }
}
