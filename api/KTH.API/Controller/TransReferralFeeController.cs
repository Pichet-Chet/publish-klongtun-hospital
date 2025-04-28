using System.IO.Compression;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.MODELS.Custom.Request.TransOrder;
using KTH.MODELS.Custom.Request.TransReferralFee;
using KTH.MODELS.Custom.Response.TransReferralFee;
using KTH.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KTH.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransReferralFeeController : ControllerBase
    {
        private readonly ITransReferralFeeService _service;

        public TransReferralFeeController(ITransReferralFeeService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTransReferralFilter([FromQuery] GetTransReferralFilterRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransReferralFilter(param);

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

        [HttpGet("history/{id}")]
        public async Task<IActionResult> GetReferralHistory(string id)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetReferralHistory(id);

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

        [HttpGet("getWaitForApprove")]
        public async Task<IActionResult> GetTransReferralWaitForApprove([FromQuery] GetTransReferralWaitForApproveRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransReferralWaitForApprove(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    resp.Rows = await _service.CountWaitForApproveAsync();

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

        [HttpGet("getTransReferralCredit")]
        public async Task<IActionResult> GetTransReferralCredit([FromQuery] GetTransReferralCreditRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransReferralCredit(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    resp.Rows = await _service.CountCreditAsync();

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

        [HttpGet("getSummary")]
        public async Task<IActionResult> GetTransSummaryReferral([FromQuery] GetTransSummaryReferralRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransSummaryReferralHeader(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    resp.Rows = await _service.CountTransSummaryReferralHeaderAsync();

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

        [HttpGet("getSummaryWaitForApprove")]
        public async Task<IActionResult> GetSummaryWaitForApprove([FromQuery] GetSummaryWaitForApproveRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetSummaryWaitForApprove(param);

                if (Outbound.Data != null)
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;

                    resp.PageSize = param.PageSize;
                    resp.PageNumber = param.PageNumber;
                    resp.Rows = await _service.CountTransSummaryWaitForApproveHeaderAsync();

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

        [HttpGet("getSummary/{id}")]
        public async Task<IActionResult> GetTransSummaryReferral(string id)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var Outbound = await _service.GetTransSummaryReferralById(id);

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

        [HttpPatch("approve")]
        public async Task<IActionResult> ApproveTransReferral([FromBody] ApproveRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.ApproveTransReferral(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpPatch("waitForApprove")]
        public async Task<IActionResult> WaitForApprove([FromBody] WaitForApproveRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.WaitForApprove(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpPost("changeReferral")]
        public async Task<IActionResult> ChangeReferral([FromBody] ChangeRegerralFeeRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.ChangeRegerralFee(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpPost("clearCredit")]
        public async Task<IActionResult> ClearCredit([FromBody] ClearCreditRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.ClearCredit(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpPost("createSummary")]
        public async Task<IActionResult> CreateTransSummary([FromBody] CreateTransSummaryRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.CreateTransSummary(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpDelete("deleteSummary/{id}")]
        public async Task<IActionResult> DeleteSummary(string id)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.DeleteSummary(id);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpPatch("waitForApproveSummary")]
        public async Task<IActionResult> WaitForApproveSummary([FromBody] WaitForApproveSummaryRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.WaitForApproveSummary(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpPatch("approveSummary")]
        public async Task<IActionResult> ApproveSummary([FromBody] ApproveSummaryRequest param)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var result = await _service.ApproveSummary(param);
                if (result.Data == null)
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
                    resp.Output = result;
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusSuccess;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode200Message;
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

        [HttpGet]
        [Route("generateTransSummaryReferralById/{id}/{masterSaleGroupId}")]
        public async Task<IActionResult> GenerateTransSummaryReferralById(string id, string masterSaleGroupId)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                var getDocument = await _service.GetTransSummaryReferralById(id.ToGuid(), masterSaleGroupId.ToGuid());

                if (getDocument.Data != null)
                {
                    var outbound = await _service.GenerateCoverSheetRefferalFeeDocument(getDocument);

                    if (outbound.Data != null && outbound.Data.StreamResults?.Count > 0)
                    {
                        if (outbound.Data.StreamResults?.Count == 1)
                        {
                            return File(outbound.Data.StreamResults[0], "image/jpeg");
                        }
                        else
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                                {
                                    for (int i = 0; i < outbound.Data.StreamResults.Count; i++)
                                    {
                                        var entry = archive.CreateEntry($"Document_{i + 1}.jpg");
                                        using (var entryStream = entry.Open())
                                        {
                                            await entryStream.WriteAsync(outbound.Data.StreamResults[i], 0, outbound.Data.StreamResults[i].Length);
                                        }
                                    }
                                }

                                Response.Clear();
                                Response.ContentType = "application/zip";
                                Response.Headers.Add("Content-Disposition", "attachment; filename=\"Document.zip\"");

                                memoryStream.Seek(0, SeekOrigin.Begin);
                                await memoryStream.CopyToAsync(Response.Body);
                                return new EmptyResult();
                            }
                        }
                    }
                    else
                    {
                        resp.Status = ConstantsResponse.StatusError;
                        resp.Code = ConstantsResponse.HttpCode200;
                        resp.Message = ConstantsResponse.HttpCode204Message;
                        resp.Output = outbound;
                    }
                }
                else
                {
                    resp.Status = ConstantsResponse.StatusError;
                    resp.Code = ConstantsResponse.HttpCode200;
                    resp.Message = ConstantsResponse.HttpCode204Message;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 error
                resp.Status = ConstantsResponse.StatusError;
                resp.Code = ConstantsResponse.HttpCode500;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            // Return error response if document generation fails
            return StatusCode(resp.Code, ApiConfiguration.GetResponseController(resp));
        }

    }
}
