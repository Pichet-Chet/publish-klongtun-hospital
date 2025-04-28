using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCase
{
    public class AutoCosultRoomRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }

        [Required(ErrorMessage = "IsPT is required")]
        public bool IsPT { get; set; }

        public bool IsPtChecked { get; set; }

        public string PtPosComment { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string UpdatedBy { get; set; } = null!;

        public string UsRemark { get; set; } = null!;

        public string UsBy { get; set; } = null!;

        public string MasterGestationalAgeId { get; set; } = null!;
        public string TransAttachMentId {  get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (IsPT)
            {
                if (string.IsNullOrEmpty(PtPosComment))
                {
                    results.Add(new ValidationResult("PtPosComment is required"));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(UsBy))
                {
                    results.Add(new ValidationResult("UsBy is required"));
                }
                else
                {
                    try
                    {
                        var usBy = new Guid(UsBy);
                    }
                    catch
                    {
                        results.Add(new ValidationResult("Invalid Guid format"));
                    }
                }

                if (string.IsNullOrEmpty(MasterGestationalAgeId))
                {
                    results.Add(new ValidationResult("MasterGestationalAgeId is required"));
                }
                else
                {
                    try
                    {
                        var usBy = new Guid(MasterGestationalAgeId);
                    }
                    catch
                    {
                        results.Add(new ValidationResult("Invalid Guid format"));
                    }
                }

                if (string.IsNullOrEmpty(TransAttachMentId))
                {
                    results.Add(new ValidationResult("TransAttachMentId is required"));
                }
                else
                {
                    try
                    {
                        var usBy = new Guid(TransAttachMentId);
                    }
                    catch
                    {
                        results.Add(new ValidationResult("Invalid Guid format"));
                    }
                }
            }
            return results;
        }
    }
}
