using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class ValidateCaseRG01Response
    {
        public ValidateCaseRG01ResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class ValidateCaseRG01ResponseData
    {
        public Guid CaseId { get; set; }
        public bool CaseIsActive { get; set; }
        public bool IsNewAppointment { get; set; }
        public DateOnly ReceiveServiceDate { get; set; }
    }
}
