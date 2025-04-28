using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransSale
{
    public class UpdateSuspendRequest
    {
        public UpdateSuspendRequest()
        {
            Suspend = true;
        }

        [Required]
        public string GroupId { get; set; } = null!;

        public bool Suspend { get; set; }

    }
}

