using KTH.REPOSITORIES.Dto;
using KTH.SERVICE.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.API.Controller
{
    [Route("api/MasterMessageConfiguration")]
    public class MasterMessageConfigurationController : ControllerBase
    {
        private readonly IMessageConfigurationCache _service;

        public MasterMessageConfigurationController(IMessageConfigurationCache service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<MasterMessageConfiguration>> GetAll()
        {
            _service.SetCache();
            return Ok(_service.Get<List<MasterMessageConfiguration>>(TextFix.TextKeyMasterMessageConfiguration));
        }

        [HttpPost("refresh")]
        public IActionResult RefreshCache()
        {
            _service.Refresh(TextFix.TextKeyMasterMessageConfiguration);
            return NoContent();
        }

        [HttpPost("clear")]
        public IActionResult ClearCache()
        {
            _service.Clear(TextFix.TextKeyMasterMessageConfiguration);
            return NoContent();
        }

    }
}
