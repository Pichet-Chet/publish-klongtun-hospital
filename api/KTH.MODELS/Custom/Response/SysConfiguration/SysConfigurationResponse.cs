using System;
using KTH.MODELS.Custom.Response.MasterThaiSubDistrict;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.SysConfiguration
{

    public class SysConfigurationResponse
    {
        public SysConfigurationResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SysConfigurationResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class SysConfigurationResponseData
	{
		public SysConfigurationResponseData()
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

