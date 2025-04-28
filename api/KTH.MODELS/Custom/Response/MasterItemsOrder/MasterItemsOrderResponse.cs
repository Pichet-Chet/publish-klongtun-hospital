using System;
using KTH.MODELS.Custom.Response.MasterHospital;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterItemsOrder
{
    public class MasterItemsOrderResponse
    {
        public MasterItemsOrderResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterItemsOrderResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterItemsOrderResponseData
	{
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public decimal Price { get; set; }

        public Guid? MasterItemsOrderGroupId { get; set; }

        public string? GroupName { get; set; }
    }
}

