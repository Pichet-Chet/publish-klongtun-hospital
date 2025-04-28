using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.TransLr;
using KTH.MODELS.Custom.Request.TransR8;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransR8Controller : ControllerBase
    {
        private readonly ITransR8Service _service;

        public TransR8Controller(ITransR8Service service)
        {
            _service = service;
        }

        [HttpGet("{caseId}")]
        public async Task<IActionResult> GetByCaseId(string caseId)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetByCaseId(caseId);

                if (Outbound.Data != null)
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
                    resp.Output = Outbound;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransR8Request param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.Create(param);

                if (Outbound.Data != null)
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
                    resp.Output = Outbound;
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
    }
}
