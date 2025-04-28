using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.MODELS.Custom.Request.TransLab
{
    public class CreateTransLabRequest : IValidatableObject
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId { get; set; }
        [Required(ErrorMessage = "LabType is Required")]
        public string LabType { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CreatedBy { get; set; }
        public List<CreateTransLabRequestItem> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (Items == null || !Items.Any())
            {
                results.Add(new ValidationResult("Items is Required"));
            }
            else
            {
                foreach (var item in Items)
                {
                    try
                    {
                        var usBy = new Guid(item.MasterItemOrderId);
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

    public class CreateTransLabRequestItem
    {
        public string MasterItemOrderId { get; set; }
    }
}
