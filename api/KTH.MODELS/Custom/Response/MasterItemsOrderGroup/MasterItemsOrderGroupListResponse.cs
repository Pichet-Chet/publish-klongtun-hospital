using System;
using KTH.MODELS.Custom.Response.MasterHospital;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterItemsOrderGroup
{
    public class MasterItemsOrderGroupListResponse
    {
        public MasterItemsOrderGroupListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterItemsOrderGroupListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterItemsOrderGroupListResponseData
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

