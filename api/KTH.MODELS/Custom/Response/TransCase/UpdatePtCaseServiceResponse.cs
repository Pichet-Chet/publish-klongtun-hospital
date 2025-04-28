using System;
namespace KTH.MODELS.Custom.Response.TransCase
{
    public class UpdatePtCaseServiceResponse
    {
        public UpdatePtCaseServiceResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class UpdatePtCaseServiceResponseData
    {
        public Guid Id { get; set; }

        public string? PtRemark { get; set; }

        public string? PtByStaffId { get; set; }
    }
}

