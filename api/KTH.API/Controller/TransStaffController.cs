using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.TransClient;
using KTH.MODELS.Custom.Request.TranStaff;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    public class TransStaffController : ControllerBase
    {
        private readonly ITranStaffService _service;

        public TransStaffController(ITranStaffService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransStaffFilter([FromQuery] GetTransStaffFilterRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransStaffFilter(param);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransStaffWithId(string id)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransStaffWithId(id);

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
        public async Task<IActionResult> CreateTransStaff([FromBody] CreateTranStaffRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.CreateTransStaff(param);
                if (result != null && result.Data != null) {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode204Message;
                    resp.Output = result;
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
        public async Task<IActionResult> UpdateTransStaff([FromBody] UpdateTranStaffRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.UpdateTransStaff(param);
                resp.Status = ConstantsResponse.StatusSuccess;
                resp.Code = ConstantsResponse.HttpCode200;
                resp.Message = ConstantsResponse.HttpCode200Message;
                resp.Output = result;
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
        [Route("changePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.UpdatePassword(param);

                resp.Status = ConstantsResponse.StatusSuccess;

                resp.Code = ConstantsResponse.HttpCode200;

                resp.Message = ConstantsResponse.HttpCode200Message;

                resp.Output = result;
            }
            catch (Exception ex)
            {
                resp.Status = ConstantsResponse.StatusError;
                resp.Code = ConstantsResponse.HttpCode500;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.Code, ApiConfiguration.GetResponseController(resp));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTranStaff(string id)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.DeleteTransStaff(id);
                resp.Status = ConstantsResponse.StatusSuccess;
                resp.Code = ConstantsResponse.HttpCode200;
                resp.Message = ConstantsResponse.HttpCode200Message;
                resp.Output = result;
            }
            catch (Exception ex)
            {
                resp.Status = ConstantsResponse.StatusError;
                resp.Code = ConstantsResponse.HttpCode500;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();

            }

            return StatusCode(resp.Code, ApiConfiguration.GetResponseController(resp));
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] GetTransStaffLoginRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransStaffLogin(param);

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
