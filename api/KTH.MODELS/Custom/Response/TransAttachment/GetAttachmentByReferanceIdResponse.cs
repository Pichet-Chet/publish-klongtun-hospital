using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransAttachment
{
    public class GetAttachmentByReferanceIdResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public List<GetAttachmentByReferanceIdResponseData> Data { get; set; }
    }

    public class GetAttachmentByReferanceIdResponseData
    {
        public Guid Id { get; set; }
        public Guid ReferanceId { get; set; }
        public string SysType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UpdatedBy {  get; set; }
        public string UpdatedDate { get; set; }
    }
}
