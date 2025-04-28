using System;
namespace KTH.MODELS.Custom.Response.TransClient
{
    public class CreateTransClientPartnerPageResponse
    {
        public CreateTransClientPartnerPageResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class CreateTransClientPartnerPageResponseData
    {
        public bool? Status { get; set; }
    }
}

