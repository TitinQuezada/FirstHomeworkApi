using Core.Contracts;
using Core.Managers;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstHomeworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityController : ControllerBase
    {
        private readonly MunicipityManager _municipityManager;
        public MunicipalityController(MunicipityManager municipityManager)
        {
            _municipityManager = municipityManager;
        }

        public async Task<ActionResult> GetAllMunicipities()
        {
            IOperationResult<IEnumerable<Municipality>> municipitiesResult = await _municipityManager.GetMunicipalities();
            if (!municipitiesResult.Success)
            {
                return BadRequest(municipitiesResult.Message);
            }

            return Ok(municipitiesResult.Entity);
        }
    }
}
