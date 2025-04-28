using System;
namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetSumOfFinanceTodayResponse
    {
        public GetSumOfFinanceTodayResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetSumOfFinanceTodayResponseData
    {
        public GetSumOfFinanceTodayResponseData()
        {
            WaitingFinancePercent = string.Empty;
            PaidOfParallelPercent = string.Empty;
            PaidDonePercent = string.Empty;
            AllPaidPercent = string.Empty;
        }

        public int WaitingFinance { get; set; } // นัดหมายวันนี้

        public string WaitingFinancePercent { get; set; } // นัดหมายวันนี้ (เปอร์เซ็น)

        public int PaidOfParallel { get; set; } // คนไข้ Walk-in

        public string PaidOfParallelPercent { get; set; }// คนไข้ Walk-in (เปอร์เซ็น)

        public int PaidDone { get; set; } // คนไข้ลงทะเบียน

        public string PaidDonePercent { get; set; } // คนไข้ลงทะเบียน (เปอร์เซ็น)

        public int AllPaid { get; set; } // คนไข้ทั้งหมดของวันนี้

        public string AllPaidPercent { get; set; } // คนไข้ทั้งหมดของวันนี้ (เปอร์เซ็น)
    }
}

