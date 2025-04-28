using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransClient
{
    public class GetTransClientWithIdResponse
    {
        public GetTransClientWithIdResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransClientWithIdResponseData
    {
        public GetTransClientWithIdResponseData()
        {
            TransClientHeader = new TransClientFilterData();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// ชื่อนาม-สกุล
        /// </summary>
        public string FullName { get; set; } = null!;

        /// <summary>
        /// เลขบัตรประชาชน
        /// </summary>
        public string? CitizenIdentification { get; set; }

        /// <summary>
        /// หนังสือเดินทาง
        /// </summary>
        public string? PassportNumber { get; set; }

        /// <summary>
        /// ที่อยู่
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// วัน เดือน ปีเกิด
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ที่ใช้ลงละเบียน
        /// </summary>
        public string TelephoneNumber { get; set; } = null!;

        /// <summary>
        /// รหัสสัญชาติ
        /// </summary>
        public Guid? MasterNationalityId { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpire { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// รหัสเบอร์โทร
        /// </summary>
        public string TelephoneCode { get; set; } = null!;

        /// <summary>
        /// เบอร์โทรสำรอง
        /// </summary>
        public string? TelephoneSecond { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string? DeletedBy { get; set; }

        /// <summary>
        /// สิทธิการรักษา
        /// </summary>
        public Guid? MasterRightTreatmentId { get; set; }

        /// <summary>
        /// รหัส Sale ค่านำพา
        /// </summary>
        public string? TranSaleRefCode { get; set; }

        /// <summary>
        /// รหัสคนไข้
        /// </summary>
        public string? HnNo { get; set; }

        /// <summary>
        /// เลขที่เอกสาร
        /// </summary>
        public string? ClientNo { get; set; }

        /// <summary>
        /// อาชีพ
        /// </summary>
        public string? Occupation { get; set; }

        public string? HostpitalName { get; set; }

        public TransClientFilterData TransClientHeader { get; set; }
    }

    public class TransClientFilterData
    {
        public string ClientNo { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string HnNo { get; set; } = null!;
        public string Age { get; set; } = null!;
        public string SaleName { get; set; } = null!;
        public string SaleGroup { get; set; } = null!;
    }
}
