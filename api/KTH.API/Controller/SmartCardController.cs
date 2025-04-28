using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartCardController : ControllerBase
    {
        private readonly ISmartCardService _service;

        public SmartCardController(ISmartCardService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ReadCard()
        {
            try
            {
                var test = _service.ReadCard();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
