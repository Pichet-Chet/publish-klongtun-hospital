using System;
namespace KTH.MODELS.Custom.Response.TransClient
{
    public class GetTransClientWithPhoneAndDateOfBirthResponse
    {
        public GetTransClientWithPhoneAndDateOfBirthResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransClientWithPhoneAndDateOfBirthResponseData
    {
        public bool isData { get; set; }
    }
}

