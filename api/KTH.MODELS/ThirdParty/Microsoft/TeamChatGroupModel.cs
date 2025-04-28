using System;
namespace KTH.MODELS.ThirdParty.Microsoft
{
	public class TeamChatGroupModel
    {
        public string? id { get; set; }
        public string? topic { get; set; }
        public DateTime? created_date_time { get; set; }
        public DateTime? last_updated_date_time { get; set; }
        public string? chat_type { get; set; }
        public string? web_url { get; set; }
        public string? tenant_id { get; set; }
        public string? branch_code { get; set; }
        public string? category { get; set; }
        public string? sender { get; set; }
        public string? is_active { get; set; }
    }
}

