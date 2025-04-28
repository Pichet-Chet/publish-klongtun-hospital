using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.TransClosePeriodIncome;
using KTH.MODELS.Custom.Response;
using KTH.MODELS.Custom.Response.TransClosePeriodIncome;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    public class TransClosePeriodIncomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITransClosePeriodIncomeService _service;

        private readonly IFinanceService _financeService;


        public TransClosePeriodIncomeController(
            IConfiguration configuration,
            IFinanceService financeService,
            ITransClosePeriodIncomeService service)
        {
            _configuration = configuration;

            _financeService = financeService;

            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterTransClosePeriodIncomeRequest param)
        {
            ResponseModel resp = new ResponseModel();

            TransClosePeriodIncomeListResponse Outbound = new TransClosePeriodIncomeListResponse();

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


        [HttpPost]
        public async Task<IActionResult> ClosePeriod([FromBody] CreateTransClosePeriodIncomeRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.Create(param);
                resp.Status = ConstantsResponse.StatusSuccess;
                resp.Code = ConstantsResponse.HttpCode200;
                resp.Message = ConstantsResponse.HttpCode200Message;
                resp.Output = Outbound;
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
        [Route("generateCoverSheetDocument/{incomeHeaderId}")]
        public async Task<IActionResult> GenerateCoverSheetDocument(string incomeHeaderId)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var GetDocument = await _service.GetById(incomeHeaderId);

                if (GetDocument.Data != null)
                {

                    var Outbound = GetDocument.Data.MoneyBucket.ToUpper() == "HOSPITAL" ? await _financeService.GenerateCoverSheetHospitalDocument(incomeHeaderId) : await _financeService.GenerateCoverSheetAssociationDocument(incomeHeaderId);

                    if (Outbound.Data != null)
                    {
                        return File((byte[])Outbound.Data.StreamResult, "image/jpeg");
                    }
                    else
                    {
                        resp.Status = ConstantsResponse.StatusError;
                        resp.Code = ConstantsResponse.HttpCode200;
                        resp.Message = ConstantsResponse.HttpCode204Message;
                        resp.Output = Outbound;
                    }
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
        [Route("generateClosePeriodSheetDocument/{incomeHeaderId}")]
        public async Task<IActionResult> GenerateClosePeriodSheetDocument(string incomeHeaderId, string actionBy)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var GetDocument = await _service.GetById(incomeHeaderId);

                if (GetDocument.Data != null)
                {

                    if (GetDocument.Data.MoneyBucket.ToLower() == "hospital")
                    {
                        var result = await _financeService.GenerateClosePeriodHospitalDocument(incomeHeaderId, actionBy);

                        if (result.Data != null)
                        {
                            var setFilename = $"ใบนำส่งรายได้แผนกวางแผน (โรงพยาบาล) :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                            return File(result.Data.StreamResult, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
                        }
                    }
                    else
                    {
                        var result = await _financeService.GenerateClosePeriodAssociationDocument(incomeHeaderId, actionBy);

                        if (result.Data != null)
                        {
                            var setFilename = $"ใบนำส่งรายได้แผนกวางแผน (สมาคม) :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                            return File(result.Data.StreamResult, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
                        }
                    }




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

