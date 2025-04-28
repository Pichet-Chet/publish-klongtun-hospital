using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.SysRole;
using KTH.MODELS.Custom.Request.MasterSaleGroup;
using KTH.MODELS.Custom.Response.SysRole;
using KTH.MODELS.Custom.Response.MasterSaleGroup;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static KTH.MODELS.Constants.ConstantsMassage;
using static KTH.MODELS.Constants.ConstantsPermission;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    public class MasterSaleGroupController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMasterSaleGroupService _service;

        public MasterSaleGroupController(IConfiguration configuration, IMasterSaleGroupService service)
        {
            _configuration = configuration;

            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterMasterSaleGroupRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MasterSaleGroupListResponse Outbound = new MasterSaleGroupListResponse();

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

            MasterSaleGroupResponse Outbound = new MasterSaleGroupResponse();

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

        [HttpGet]
        [Route("byName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            ResponseModel resp = new ResponseModel();

            MasterSaleGroupResponse Outbound = new MasterSaleGroupResponse();

            try
            {
                Outbound = await _service.GetByName(name);

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
        public async Task<IActionResult> Create([FromBody] CreateMasterSaleGroupRequest request)
        {
            ResponseModel resp = new ResponseModel();

            MasterSaleGroupResponse Outbound = new MasterSaleGroupResponse();

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
        public async Task<IActionResult> Update([FromBody] UpdateMasterSaleGroupRequest request)
        {
            ResponseModel resp = new ResponseModel();

            MasterSaleGroupResponse Outbound = new MasterSaleGroupResponse();

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

