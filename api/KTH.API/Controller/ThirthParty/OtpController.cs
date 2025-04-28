using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Response.MasterCountry;
using KTH.MODELS.ThirdParty.SMSMKT;
using KTH.SERVICE;
using KTH.SERVICE.ThirthParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KTH.API.Controller.ThirthParty
{
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISmsMktService _service;

        public OtpController(IConfiguration configuration, ISmsMktService service)
        {
            _configuration = configuration;

            _service = service;
        }

        [HttpPost]
        [Route("thailand")]
        [AllowAnonymous]
        public async Task<IActionResult> OTPSend([FromBody] Request param)
        {
            ResponseModel resp = new ResponseModel();

            OtpSendReturn Outbound = new OtpSendReturn();

            try
            {
                Outbound = await _service.OTPSend(param.Phone);

                if (Outbound != null)
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

        [HttpPost]
        [Route("inter")]
        [AllowAnonymous]
        public async Task<IActionResult> OTPSendInter([FromBody] Request param)
        {
            ResponseModel resp = new ResponseModel();

            OtpSendReturn Outbound = new OtpSendReturn();

            try
            {
                Outbound = await _service.OTPSendInter(param);

                if (Outbound != null)
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

        [HttpPost]
        [Route("validate")]
        [AllowAnonymous]
        public async Task<IActionResult> OTPValidate([FromBody] OtpValidateRequest param)
        {
            ResponseModel resp = new ResponseModel();

            OtpValidateReturn Outbound = new OtpValidateReturn();

            try
            {
                Outbound = await _service.OTPValidate(param);

                if (Outbound != null)
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

    }
}

