using Core.Contracts;
using Core.Managers;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("{userId}")]
        public async Task <IActionResult> GetUser(string userId)
        {
            IOperationResult<ApplicationUserViewModel> userResult = await _applicationUserManager.GetUser(userId);
            if (!userResult.Success)
            {
                return BadRequest(userResult.Message);
            }

            return Ok(userResult.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            IOperationResult<List<ApplicationUserViewModel>> getUsersResult = await _applicationUserManager.GetUsers();
            if (!getUsersResult.Success)
            {
                return BadRequest(getUsersResult.Message);
            }

            return Ok(getUsersResult.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(ApplicationUserCreateViewModel user)
        {
            IOperationResult<string> userToUpdateResult = await _applicationUserManager.UpdateUser(user);

            if (!userToUpdateResult.Success)
            {
                return BadRequest(userToUpdateResult.Message);
            }

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser (string userId)
        {
            IOperationResult<string> deletedUserResult = await _applicationUserManager.DeleteUser(userId);

            if (!deletedUserResult.Success)
            {
                return BadRequest(deletedUserResult.Message);
            }

            return Ok();
        }

        [HttpGet("validate-user-existence/{email}")]
        public async Task<IActionResult> ValidateUserExistence(string email)
        {
            IOperationResult<bool> validateuserExistenceResult = await _applicationUserManager.ValidateUserExist(email);

            if (!validateuserExistenceResult.Success)
            {
                return BadRequest(validateuserExistenceResult.Message);
            }

            return Ok(validateuserExistenceResult.Entity);
        }
    }
}
