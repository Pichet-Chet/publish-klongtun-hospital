using System;
namespace KTH.MODELS.Custom.Response.TransClient
{
    public class GetTransClientWithPhoneAndRegisterCaseResponse
    {
        public GetTransClientWithPhoneAndRegisterCaseResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransClientWithPhoneAndRegisterCaseResponseData
    {
        public bool isRg01 { get; set; }

        public string FullName { get; set; }

        public DateOnly DateOfBirth { get; set; }
    }
}

