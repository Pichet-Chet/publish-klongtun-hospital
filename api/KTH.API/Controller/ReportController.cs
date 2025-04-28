using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.Report;
using KTH.MODELS.Custom.Request.Report.Executive;
using KTH.MODELS.Custom.Response.Report;
using KTH.MODELS.Custom.Response.Report.Executive;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IReportService _service;

        public ReportController(IConfiguration configuration, IReportService service)
        {
            _configuration = configuration;

            _service = service;
        }

        #region Response

        [HttpGet]
        [Route("monthlyAppointmentReport")] // Report No.1
        public async Task<IActionResult> MonthlyAppointmentReport([FromQuery] MonthlyAppointmentReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MonthlyAppointmentReportResponse Outbound = new MonthlyAppointmentReportResponse();

            try
            {
                Outbound = await _service.MonthlyAppointmentReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("consultStaffCaseReport")] // Report No.2
        public async Task<IActionResult> ConsultStaffCaseReport([FromQuery] ConsultStaffCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            ConsultStaffCaseReportResponse Outbound = new ConsultStaffCaseReportResponse();

            try
            {
                Outbound = await _service.ConsultStaffCaseReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("foreignClientReport")] // Report No.3
        public async Task<IActionResult> ForeignClientReport([FromQuery] ForeignClientReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            ForeignClientReportResponse Outbound = new ForeignClientReportResponse();

            try
            {
                Outbound = await _service.ForeignClientReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("caseByTypeReport")] // Report No.4
        public async Task<IActionResult> CaseByTypeReport([FromQuery] CaseByTypeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            CaseByTypeReportResponse Outbound = new CaseByTypeReportResponse();

            try
            {
                Outbound = await _service.CaseByTypeReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("yearlyCaseReport")] // Report No.5
        public async Task<IActionResult> YearlyCaseReport([FromQuery] YearlyCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            YearlyCaseReportResponse Outbound = new YearlyCaseReportResponse();

            try
            {
                Outbound = await _service.YearlyCaseReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("dailyCaseByServiceReport")] // Report No.6
        public async Task<IActionResult> DailyCaseByServiceReport([FromQuery] DailyCaseByServiceReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            DailyCaseByServiceReportResponse Outbound = new DailyCaseByServiceReportResponse();

            try
            {
                Outbound = await _service.DailyCaseByServiceReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("monthlyRefusalCaseReport")] // Report No.8
        public async Task<IActionResult> MonthlyRefusalCaseReport([FromQuery] MonthlyRefusalCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MonthlyRefusalCaseReportResponse Outbound = new MonthlyRefusalCaseReportResponse();

            try
            {
                Outbound = await _service.MonthlyRefusalCaseReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("dailyCaseReport")] // Report No.9
        public async Task<IActionResult> DailyCaseReport([FromQuery] DailyCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            DailyCaseReportResponse Outbound = new DailyCaseReportResponse();

            try
            {
                Outbound = await _service.DailyCaseReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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

        #endregion

        #region Export

        [HttpPost]
        [Route("monthlyAppointmentReportExport")] // Report No.1
        public async Task<IActionResult> MonthlyAppointmentReportExport([FromQuery] MonthlyAppointmentReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.MonthlyAppointmentReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานสรุปยอดคนไข้นัดประจําเดือน :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("consultStaffCaseReportExport")] // Report No.2
        public async Task<IActionResult> ConsultStaffCaseReportExport([FromQuery] ConsultStaffCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.ConsultStaffCaseReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานสรุปการปฎิบัติงานของเจ้าหน้าที่ Consult :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("foreignClientReportExport")] // Report No.3
        public async Task<IActionResult> ForeignClientReportExport([FromQuery] ForeignClientReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            ForeignClientReportResponse Outbound = new ForeignClientReportResponse();

            try
            {
                var result = await _service.ForeignClientReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"ยอดคนไข้ต่างชาติ :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("caseByTypeReportExport")] // Report No.4
        public async Task<IActionResult> CaseByTypeReportExport([FromQuery] CaseByTypeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.CaseByTypeReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานสรุปการรักษา :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("yearlyCaseReportExport")] // Report No.5
        public async Task<IActionResult> YearlyCaseReportExport([FromQuery] YearlyCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            YearlyCaseReportExportResponse Outbound = new YearlyCaseReportExportResponse();

            try
            {
                var result = await _service.YearlyCaseReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานสรุปยอดคนไข้รายปี :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("dailyCaseByServiceReportExport")] // Report No.6
        public async Task<IActionResult> DailyCaseByServiceReportExport([FromQuery] DailyCaseByServiceReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            DailyCaseByServiceReportExportResponse Outbound = new DailyCaseByServiceReportExportResponse();

            try
            {
                var result = await _service.DailyCaseByServiceReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานสรุปยอดการเข้ามารับบริการ :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("monthlyRefusalCaseReportExport")] // Report No.8
        public async Task<IActionResult> MonthlyRefusalCaseReportExport([FromQuery] MonthlyRefusalCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MonthlyRefusalCaseReportExportResponse Outbound = new MonthlyRefusalCaseReportExportResponse();

            try
            {
                var result = await _service.MonthlyRefusalCaseReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานสาเหตุที่แพทย์ไม่รับรายเดือน :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("dailyCaseReportExport")] // Report No.9
        public async Task<IActionResult> DailyCaseReportExport([FromQuery] DailyCaseReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            DailyCaseReportExportResponse Outbound = new DailyCaseReportExportResponse();

            try
            {
                var result = await _service.DailyCaseReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานเคสรายวัน :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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


        #endregion


        #region Response Executive

        [HttpGet]
        [Route("monthlyPatientByStoreReport")] // Report No.1
        public async Task<IActionResult> MonthlyPatientByStoreReport([FromQuery] MonthlyPatientByStoreReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MonthlyPatientByStoreReportResponse Outbound = new MonthlyPatientByStoreReportResponse();

            try
            {
                Outbound = await _service.MonthlyPatientByStoreReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("monthlyStoreCountAndCarriageFeeReport")] // Report No.2
        public async Task<IActionResult> MonthlyStoreCountAndCarriageFeeReport([FromQuery] MonthlyStoreCountAndCarriageFeeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MonthlyStoreCountAndCarriageFeeReportResponse Outbound = new MonthlyStoreCountAndCarriageFeeReportResponse();

            try
            {
                Outbound = await _service.MonthlyStoreCountAndCarriageFeeReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("weeklyStoreCountAndCarriageFeeReport")] // Report No.3
        public async Task<IActionResult> WeeklyStoreCountAndCarriageFeeReport([FromQuery] WeeklyStoreCountAndCarriageFeeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            WeeklyStoreCountAndCarriageFeeReportResponse Outbound = new WeeklyStoreCountAndCarriageFeeReportResponse();

            try
            {
                Outbound = await _service.WeeklyStoreCountAndCarriageFeeReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("weeklyStoreCarriageFeeReport")] // Report No.4
        public async Task<IActionResult> WeeklyStoreCarriageFeeReport([FromQuery] WeeklyStoreCarriageFeeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            WeeklyStoreCarriageFeeReportResponse Outbound = new WeeklyStoreCarriageFeeReportResponse();

            try
            {
                Outbound = await _service.WeeklyStoreCarriageFeeReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("quarterlyPatientStatisticsReport")] // Report No.5
        public async Task<IActionResult> QuarterlyPatientStatisticsReport([FromQuery] QuarterlyPatientStatisticsReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            QuarterlyPatientStatisticsReportResponse Outbound = new QuarterlyPatientStatisticsReportResponse();

            try
            {
                Outbound = await _service.QuarterlyPatientStatisticsReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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
        [Route("monthlyPatientStatisticsReport")] // Report No.6
        public async Task<IActionResult> MonthlyPatientStatisticsReport([FromQuery] MonthlyPatientStatisticsReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            MonthlyPatientStatisticsReportResponse Outbound = new MonthlyPatientStatisticsReportResponse();

            try
            {
                Outbound = await _service.MonthlyPatientStatisticsReport(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    //resp.Rows = await _service.CountAllAsync();

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

        #endregion

        #region Export Executive

        [HttpPost]
        [Route("monthlyPatientByStoreReportExport")] // Report No.1
        public async Task<IActionResult> MonthlyPatientByStoreReportExport([FromQuery] MonthlyPatientByStoreReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.MonthlyPatientByStoreReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานยอดคนไข้รายเดือนตามร้านค้า :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("monthlyStoreCountAndCarriageFeeReportExport")] // Report No.2
        public async Task<IActionResult> MonthlyStoreCountAndCarriageFeeReportExport([FromQuery] MonthlyStoreCountAndCarriageFeeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.MonthlyStoreCountAndCarriageFeeReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานจำนวนร้านค้าและค่านำพารายเดือน :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("weeklyStoreCountAndCarriageFeeReportExport")] // Report No.3
        public async Task<IActionResult> WeeklyStoreCountAndCarriageFeeReportExport([FromQuery] WeeklyStoreCountAndCarriageFeeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.WeeklyStoreCountAndCarriageFeeReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานจำนวนร้านค้าและค่านำพารายสัปดาห์ :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("weeklyStoreCarriageFeeReportExport")] // Report No.4
        public async Task<IActionResult> WeeklyStoreCarriageFeeReportExport([FromQuery] WeeklyStoreCarriageFeeReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.WeeklyStoreCarriageFeeReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"รายงานค่านำพารายสัปดาห์ของร้านค้า :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("quarterlyPatientStatisticsReportExport")] // Report No.5
        public async Task<IActionResult> QuarterlyPatientStatisticsReportExport([FromQuery] QuarterlyPatientStatisticsReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.QuarterlyPatientStatisticsReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"สถิติคนไข้รายไตรมาส :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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
        [Route("monthlyPatientStatisticsReportExport")] // Report No.6
        public async Task<IActionResult> MonthlyPatientStatisticsReportExport([FromQuery] MonthlyPatientStatisticsReportRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.MonthlyPatientStatisticsReportExport(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    var setFilename = $"สถิติคนไข้รายเดือน :{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{setFilename}.xlsx");
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

        #endregion
    }
}

