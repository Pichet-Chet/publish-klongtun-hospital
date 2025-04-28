using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.TransConsultRoom;
using KTH.MODELS.Custom.Response.TransConsultRoom;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    public class TransConsultRoomController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITransConsultRoomService _service;

        public TransConsultRoomController(IConfiguration configuration, ITransConsultRoomService service)
        {
            _configuration = configuration;

            _service = service;
        }


        [HttpGet]
        [Route("caseId/{caseId}")]
        public async Task<IActionResult> GetByClientId(string caseId)
        {
            ResponseModel resp = new ResponseModel();

            TransConsultRoomResponse Outbound = new TransConsultRoomResponse();

            try
            {
                Outbound = await _service.GetByCaseId(caseId);

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
        public async Task<IActionResult> Create([FromBody] CreateTransConsultRoomRequest request)
        {
            ResponseModel resp = new ResponseModel();

            TransConsultRoomResponse Outbound = new TransConsultRoomResponse();

            try
            {
                Outbound = await _service.Create(request);

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

