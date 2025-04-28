

namespace KTH.MODELS.Custom.Response.TransAttachment
{
    public class DeleteAttachmentResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public DeleteAttachmentResponseData Data { get; set; }

    }

    public class DeleteAttachmentResponseData
    {
        public Guid Id { get; set; }
    }
}
