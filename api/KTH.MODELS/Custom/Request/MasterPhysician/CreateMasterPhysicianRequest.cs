using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterPhysician
{
    public class CreateMasterPhysicianRequest
	{
		public CreateMasterPhysicianRequest()
		{
		}

        [Required]
        public string Prefix { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}

