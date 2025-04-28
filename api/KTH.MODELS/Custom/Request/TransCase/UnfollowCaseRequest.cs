using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransCase
{
    public class UnfollowCaseRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId {  get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterUnFollowId {  get; set; }
        public string RemarkUnfollow {  get; set; }
    }
}
