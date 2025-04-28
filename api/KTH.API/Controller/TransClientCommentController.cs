using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.MasterNationality;
using KTH.MODELS.Custom.Request.TransClientComment;
using KTH.MODELS.Custom.Response.MasterNationality;
using KTH.MODELS.Custom.Response.TransClientComment;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    public class TransClientCommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITransClientCommentService _service;

        public TransClientCommentController(IConfiguration configuration, ITransClientCommentService service)
        {
            _configuration = configuration;

            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterTransClientCommentRequest param)
        {
            ResponseModel resp = new ResponseModel();

            TransClientCommentListResponse Outbound = new TransClientCommentListResponse();

            try
            {
                Outbound = await _service.GetAll(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    resp.Rows = await _service.CountAllAsync();

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


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseModel resp = new ResponseModel();

            TransClientCommentResponse Outbound = new TransClientCommentResponse();

            try
            {
                Outbound = await _service.GetById(id);

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
        public async Task<IActionResult> Create([FromBody] CreateTransClientCommentRequest request)
        {
            ResponseModel resp = new ResponseModel();

            TransClientCommentResponse Outbound = new TransClientCommentResponse();

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

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateTransClientCommentRequest request)
        {
            ResponseModel resp = new ResponseModel();

            TransClientCommentResponse Outbound = new TransClientCommentResponse();

            try
            {
                Outbound = await _service.Update(request);

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

