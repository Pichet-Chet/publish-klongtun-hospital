using System;
using KTH.MODELS.Custom.Response.MasterThaiSubDistrict;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.SysConfiguration
{

    public class SysConfigurationListResponse
    {
        public SysConfigurationListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SysConfigurationListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class SysConfigurationListResponseData
	{
		public SysConfigurationListResponseData()
		{
            Key = string.Empty;

            Value = string.Empty;
		}

        public Guid Id { get; set; }

        public string Key { get; set; } 

        public string Value { get; set; }

        public string? Group { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}

