using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS
{
	public class ResponseModel
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("Code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; } = string.Empty;

        [JsonPropertyName("innerException")]
        public string? InnerException { get; set; }

        [JsonPropertyName("pageNumber")]
        public int? PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        [JsonPropertyName("rows")]
        public int? Rows { get; set; }

        [JsonPropertyName("output")]
        public object? Output { get; set; }
    }
}

