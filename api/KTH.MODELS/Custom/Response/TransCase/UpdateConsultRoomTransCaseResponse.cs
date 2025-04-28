using System;
namespace KTH.MODELS.Custom.Response.TransCase
{
    public class UpdateConsultRoomTransCaseResponse
    {
        public UpdateConsultRoomTransCaseResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class UpdateConsultRoomTransCaseResponseData
    {
        public UpdateConsultRoomTransCaseResponseData()
        {

        }

        public Guid? MasterConsultRoomId { get; set; }
        public string? MasterConsultRoomName { get; set; }
    }


}

