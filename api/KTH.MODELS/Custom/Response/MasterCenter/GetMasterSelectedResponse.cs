using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.MasterCenter
{
    public class GetMasterSelectedResponse
    {
        public List<GetMasterSelectedResponseData> Data {  get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetMasterSelectedResponseData
    {
        public string MasterName { get; set; }
        public object Items { get; set; }
    }

}
