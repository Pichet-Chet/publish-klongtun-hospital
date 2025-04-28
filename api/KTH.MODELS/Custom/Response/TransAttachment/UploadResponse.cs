using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransAttachment
{
    public class UploadResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public UploadResponseData Data { get; set; }
    }

    public class UploadResponseData
    {
        public Guid Id { get; set; }
    }
}
