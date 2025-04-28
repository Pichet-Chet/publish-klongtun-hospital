using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterConsultRoom
{
	public class UpdateMasterConsultRoomRequest
	{
        private static readonly string[] AllowedTypes = { "General", "Manager" };

        private string? _type;

        public UpdateMasterConsultRoomRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string? Type
        {
            get => _type;
            set
            {
                if (value != null && !AllowedTypes.Contains(value))
                {
                    throw new ArgumentException("Invalid value for Type. Allowed values are 'General' or 'Manager'.");
                }
                _type = value;
            }
        }

        public int? Seq { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

