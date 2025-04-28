using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.ThirdParty.SMSMKT;
using KTH.MODELS.ThirdParty.SMTP;
using KTH.SERVICE.ThirthParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KTH.API.Controller.ThirthParty
{
    [Route("api/[controller]")]
    public class SmtpEmailController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGmailSmtpService _service;

        public SmtpEmailController(IConfiguration configuration, IGmailSmtpService service)
        {
            _configuration = configuration;

            _service = service;
        }

        [HttpPost]
        [Route("alertSaleAccountCreate")]
        [AllowAnonymous]
        public async Task<IActionResult> AlertSaleAccountCreate([FromBody] SmtpSendModel param)
        {
            ResponseModel resp = new ResponseModel();

            bool Outbound = false;

            try
            {
                Outbound = await _service.AlertSaleAccountCreate(param);

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

        
    }
}

