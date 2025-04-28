using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.SERVICE.AutoInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KTH.API.Controller.AutoInterface
{
    [Route("api/[controller]")]
    public class AutoInterfaceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAutoInterfaceService _service;

        public AutoInterfaceController(IConfiguration configuration, IAutoInterfaceService service)
        {
            _configuration = configuration;

            _service = service;
        }

        [HttpPatch]
        [Route("DisabledCaseAuto")]
        [AllowAnonymous]
        public async Task<IActionResult> DisabledCaseAuto()
        {
            ResponseModel resp = new ResponseModel();

            bool Outbound = false;

            try
            {
                Outbound = await _service.DisabledCaseAuto();

                if (Outbound)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode204Message;
                    resp.Output = null;
                }
            }
            catch (Exception ex)
            {
                resp.Status = ConstantsResponse.StatusError;
                resp.Code = ConstantsResponse.HttpCode500;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();

            }

            return StatusCode(resp.Code, ApiConfiguration.GetResponseController(resp));
        }

        [HttpPatch]
        [Route("FinishedCaseAuto")]
        [AllowAnonymous]
        public async Task<IActionResult> FinishedCaseAuto()
        {
            ResponseModel resp = new ResponseModel();

            bool Outbound = false;

            try
            {
                Outbound = await _service.FinishedCaseAuto();

                if (Outbound)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode204Message;
                    resp.Output = null;
                }
            }
            catch (Exception ex)
            {
                resp.Status = ConstantsResponse.StatusError;
                resp.Code = ConstantsResponse.HttpCode500;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();

            }

            return StatusCode(resp.Code, ApiConfiguration.GetResponseController(resp));
        }

        [HttpGet("TestCreateCase/{count}")]
        [AllowAnonymous]
        public async Task<IActionResult> TestCreateCase(int count)
        {
            ResponseModel resp = new ResponseModel();
            try
            {
                await _service.TestCreateCase(count);
                resp.Status = ConstantsResponse.StatusSuccess;
                resp.Code = ConstantsResponse.HttpCode200;
                resp.Message = ConstantsResponse.HttpCode200Message;
            }
            catch (Exception ex)
            {
                resp.Status = ConstantsResponse.StatusError;
                resp.Code = ConstantsResponse.HttpCode500;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }
            return StatusCode(resp.Code, ApiConfiguration.GetResponseController(resp));
        }

    }
}

