using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransLab
{
    public class UpdateTransLabRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }
        public bool IsSend { get; set; }
        public bool IsCompleted { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string UpdateBy { get; set; }

    }
}
