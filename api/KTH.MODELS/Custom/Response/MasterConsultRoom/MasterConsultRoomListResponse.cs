using System;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.TransClient;

namespace KTH.MODELS.Custom.Response.MasterConsultRoom
{
    public class MasterConsultRoomListResponse
    {
        public MasterConsultRoomListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterConsultRoomListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }


    public class MasterConsultRoomListResponseData
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? Owner { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string OwnerName { get; set; }
    }

}

