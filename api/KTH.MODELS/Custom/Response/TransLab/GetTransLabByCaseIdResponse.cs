using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransLab
{
    public class GetTransLabByCaseIdResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public List<GetTransLabByCaseIdResponseData> Data { get; set; }
    }

    public class GetTransLabByCaseIdResponseData
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public string LabType { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public bool IsSend { get; set; }
        public bool IsCompleted { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public List<LabAttachment> Attachments { get; set; }
    }

    public class LabAttachment
    {
        public Guid? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentName { get; set; }
    }
}
