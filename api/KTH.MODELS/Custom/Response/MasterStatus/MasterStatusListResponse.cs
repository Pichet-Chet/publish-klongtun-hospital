using System;
using KTH.MODELS.Custom.Response.MasterSaleGroup;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterStatus
{
    public class MasterStatusListResponse
    {
        public MasterStatusListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterStatusListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterStatusListResponseData
	{
        public Guid Id { get; set; }

        public string Group { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

