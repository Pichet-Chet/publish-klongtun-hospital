
namespace KTH.MODELS.Custom.Response.TransLab
{
    public class UpdateTransLabResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public UpdateTransLabResponseData Data { get; set; }
    }

    public class UpdateTransLabResponseData
    {
        public Guid Id { get; set; }
    }
}
