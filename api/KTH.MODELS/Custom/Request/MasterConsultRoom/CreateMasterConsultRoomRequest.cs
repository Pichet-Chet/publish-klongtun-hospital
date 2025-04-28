using System;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.MODELS.Custom.Request.MasterConsultRoom
{
	public class CreateMasterConsultRoomRequest
	{
        private static readonly string[] AllowedTypes = { "General", "Manager" };

        private string? _type;

        public CreateMasterConsultRoomRequest()
		{

		}

        public string? Type
        {
            get => _type;
            set
            {
                if (value != null && !AllowedTypes.Contains(value))
                {
                    throw new Exception("Invalid value for Type. Allowed values are 'General' or 'Manager'.");
                }
                _type = value;
            }
        }

        public int? Seq { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }
    }
}

