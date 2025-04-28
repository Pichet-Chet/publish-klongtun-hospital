using KTH.MODELS.Helper;

namespace KTH.MODELS.Constants
{
    public class ConstantsMassage
    {
        #region | Status Flow | CASE
        public class StatusFlowCase : Enumeration
        {
            // D151 : ส่วนงาน 151
            // GRC ห้องคอนเซาต์
            // MNC ห้องคอนเซาต์เมเนเจอร์


            public static StatusFlowCase Register = new("RG-01", "Register");
            public static StatusFlowCase D151x01 = new("D151-01", "151 รับลูกค้าแล้ว");
            public static StatusFlowCase D151x02 = new("D151-02", "รอรับเงิน U/S");

            public static StatusFlowCase PT151x01 = new("PT151-01", "รอเปิดออเดอร์ PT");
            public static StatusFlowCase PT151x02 = new("PT151-02", "รอเก็บเงิน PT");
            public static StatusFlowCase PT151x03 = new("PT151-03", "รอทำ PT");


            public static StatusFlowCase D151x03 = new("D151-03", "รอ U/S");
            public static StatusFlowCase D151x04 = new("D151-04", "จองคิว นัดหมาย (ฟรี U/S)");
            public static StatusFlowCase USx01 = new("US-01", "รอ Consult");
            public static StatusFlowCase GRCx01 = new("GRC-01", "อยู่ระหว่างปรึกษา Consult General");
            public static StatusFlowCase GRCx02 = new("GRC-02", "ส่งต่อห้อง Manager");
            public static StatusFlowCase GRCx03 = new("GRC-03", "รออนุมัติส่วนลด");
            public static StatusFlowCase GRCx04 = new("GRC-04", "รอชำระเงิน");
            public static StatusFlowCase GRCx05 = new("GRC-05", "ไม่อนุมัติ/รอแก้ไข");
            public static StatusFlowCase MNCx01 = new("MNC-01", "อยู่ระหว่างปรึกษา Consult Manager");
            public static StatusFlowCase MNCx02xH = new("MNC-02-H", "ไม่่เข้ารับการรักษา");
            public static StatusFlowCase MNCx02 = new("MNC-02", "ไม่่เข้ารับการรักษา");
            public static StatusFlowCase MNCx03xH = new("MNC-03-H", "นััดหมายใหม่");
            public static StatusFlowCase MNCx03 = new("MNC-03", "นััดหมายใหม่");
            public static StatusFlowCase GMSx04 = new("MNS-04", "รอชำระเงิน");
            public static StatusFlowCase FNx01 = new("FN-01", "ชำระเงินแล้วเต็มจำนวน");
            public static StatusFlowCase FNx02 = new("FN-02", "ชำระเงินแล้วบางส่วน");
            public static StatusFlowCase SYSx01 = new("SYS-01", "เสร็จสิ้นการรักษา");
            public static StatusFlowCase SYSx02 = new("SYS-02", "ยกเลิกเคสโดยระบบ(ครบ 15 วัน)");
            public static StatusFlowCase SYSx03 = new("SYS-03", "คืนเงิน");

            public static StatusFlowCase ORx01 = new("OR-01", "รอชำระเงิน");
            public static StatusFlowCase ORx02 = new("OR-02", "ชำระเงินสำเร็จ");
            public static StatusFlowCase ORx03 = new("OR-03", "ชำระเงินบางส่วน");
            public static StatusFlowCase ORx04 = new("OR-04", "ฉบับร่าง");
            public static StatusFlowCase ORx05 = new("OR-05", "รออนุมัติส่วนลด");
            public static StatusFlowCase ORx06 = new("OR-06", "คืนเงินเต็มจำนวน ปิดเคส");
            public static StatusFlowCase ORx07 = new("OR-07", "คืนเงินบางส่วน ปิดเคส");
            public static StatusFlowCase ORx99 = new("OR-99", "ชำระเงินสำเร็จ");

            public static StatusFlowCase CSx01 = new("CS-01", "ลูกค้ายกเลิกนัดหมาย");
            public static StatusFlowCase RFx01 = new("RF-01", "รอคืนเงิน");
            public static StatusFlowCase RFx02 = new("RF-02", "คืนเงิน/ปิดเคส");

            public static StatusFlowCase CSPx01 = new("CSP-01", "ยกเลิกการชำระ");

            public static StatusFlowCase RFFx01 = new("RFF-01", "Create");
            public static StatusFlowCase RFFx02 = new("RFF-02", "รอดำเนินการ");
            public static StatusFlowCase RFFx03 = new("RFF-03", "รออนุมัติ");
            public static StatusFlowCase RFFx04 = new("RFF-04", "รออนุมัติ");
            public static StatusFlowCase RFFx05 = new("RFF-05", "ทำใบสำคัญจ่ายแล้ว");
            public static StatusFlowCase RFFx06 = new("RFF-06", "จัดทำใบค่านำพาแล้ว");
            public static StatusFlowCase RFFx07 = new("RFF-07", "ไม่อนุมัติ");

            public static StatusFlowCase SRFFx01 = new("SRFF-01", "รออนุมัติ");
            public static StatusFlowCase SRFFx02 = new("SRFF-02", "อนุมัติแล้ว");
            public static StatusFlowCase SRFFx03 = new("SRFF-03", "พิมพ์ใบสำคัญจ่ายแล้ว");
            public static StatusFlowCase SRFFx04 = new("SRFF-04", "ไม่อนุมัติ");
            public static StatusFlowCase SRFFx05 = new("SRFF-05", "Create");

            public static StatusFlowCase Lx01 = new("L-01", "รอ 151 ส่ง Lab");
            public static StatusFlowCase Lx02 = new("L-02", "ส่งตรวจแล้ว / รอผล Lab");
            public static StatusFlowCase R8Lx01 = new("R8L-01", "รอ 151 ส่ง Lab");
            public static StatusFlowCase R8Lx02 = new("R8L-02", "ส่งตรวจแล้ว / รอผล Lab");
            public static StatusFlowCase R8x01 = new("R8-01", "รอ R8");
            public static StatusFlowCase R8x02 = new("R8-02", "ระหว่าง R8");
            public static StatusFlowCase R8USx01 = new("R8US-01", "รอ Consult");
            public static StatusFlowCase R8GRCx01 = new("R8GRC-01", "อยู่ระหว่างปรึกษา Consult General");
            public static StatusFlowCase LR2x01 = new("LR2-01", "รอ 151 ส่ง LR2");
            public static StatusFlowCase LR2x02 = new("LR2-02", "ส่ง LR2 แล้ว");



            public StatusFlowCase(string Key, string Value) : base(Key, Value)
            {
            }

            public static string GetValue(string key)
            {
                if (key == StatusFlowCase.Register.Key) { return StatusFlowCase.Register.Value; }
                if (key == StatusFlowCase.D151x01.Key) { return StatusFlowCase.D151x01.Value; }
                if (key == StatusFlowCase.D151x02.Key) { return StatusFlowCase.D151x02.Value; }
                if (key == StatusFlowCase.D151x03.Key) { return StatusFlowCase.D151x03.Value; }
                if (key == StatusFlowCase.D151x04.Key) { return StatusFlowCase.D151x04.Value; }
                if (key == StatusFlowCase.USx01.Key) { return StatusFlowCase.USx01.Value; }
                if (key == StatusFlowCase.GRCx01.Key) { return StatusFlowCase.GRCx01.Value; }
                if (key == StatusFlowCase.GRCx02.Key) { return StatusFlowCase.GRCx02.Value; }
                if (key == StatusFlowCase.GRCx03.Key) { return StatusFlowCase.GRCx03.Value; }
                if (key == StatusFlowCase.GRCx04.Key) { return StatusFlowCase.GRCx04.Value; }
                if (key == StatusFlowCase.GRCx05.Key) { return StatusFlowCase.GRCx05.Value; }
                if (key == StatusFlowCase.MNCx01.Key) { return StatusFlowCase.MNCx01.Value; }
                if (key == StatusFlowCase.MNCx02xH.Key) { return StatusFlowCase.MNCx02xH.Value; }
                if (key == StatusFlowCase.MNCx02.Key) { return StatusFlowCase.MNCx02.Value; }
                if (key == StatusFlowCase.MNCx03xH.Key) { return StatusFlowCase.MNCx03xH.Value; }
                if (key == StatusFlowCase.MNCx03.Key) { return StatusFlowCase.MNCx03.Value; }
                if (key == StatusFlowCase.GMSx04.Key) { return StatusFlowCase.GMSx04.Value; }
                if (key == StatusFlowCase.FNx01.Key) { return StatusFlowCase.FNx01.Value; }
                if (key == StatusFlowCase.FNx02.Key) { return StatusFlowCase.FNx02.Value; }
                if (key == StatusFlowCase.SYSx01.Key) { return StatusFlowCase.SYSx01.Value; }
                if (key == StatusFlowCase.SYSx02.Key) { return StatusFlowCase.SYSx02.Value; }
                if (key == StatusFlowCase.SYSx03.Key) { return StatusFlowCase.SYSx03.Value; }
                if (key == StatusFlowCase.ORx01.Key) { return StatusFlowCase.ORx01.Value; }
                if (key == StatusFlowCase.ORx02.Key) { return StatusFlowCase.ORx02.Value; }
                if (key == StatusFlowCase.ORx03.Key) { return StatusFlowCase.ORx03.Value; }
                if (key == StatusFlowCase.ORx04.Key) { return StatusFlowCase.ORx04.Value; }
                if (key == StatusFlowCase.ORx05.Key) { return StatusFlowCase.ORx05.Value; }
                if (key == StatusFlowCase.ORx06.Key) { return StatusFlowCase.ORx06.Value; }
                if (key == StatusFlowCase.ORx07.Key) { return StatusFlowCase.ORx07.Value; }
                if (key == StatusFlowCase.CSx01.Key) { return StatusFlowCase.CSx01.Value; }
                if (key == StatusFlowCase.PT151x01.Key) { return StatusFlowCase.PT151x01.Value; }
                if (key == StatusFlowCase.PT151x02.Key) { return StatusFlowCase.PT151x02.Value; }
                if (key == StatusFlowCase.PT151x03.Key) { return StatusFlowCase.PT151x03.Value; }

                if (key == StatusFlowCase.RFFx01.Key) { return StatusFlowCase.RFFx01.Value; }
                if (key == StatusFlowCase.RFFx02.Key) { return StatusFlowCase.RFFx02.Value; }
                if (key == StatusFlowCase.RFFx03.Key) { return StatusFlowCase.RFFx03.Value; }
                if (key == StatusFlowCase.RFFx04.Key) { return StatusFlowCase.RFFx04.Value; }
                if (key == StatusFlowCase.RFFx05.Key) { return StatusFlowCase.RFFx05.Value; }
                if (key == StatusFlowCase.RFFx06.Key) { return StatusFlowCase.RFFx06.Value; }
                if (key == StatusFlowCase.RFFx07.Key) { return StatusFlowCase.RFFx07.Value; }

                if (key == StatusFlowCase.SRFFx01.Key) { return StatusFlowCase.SRFFx01.Value; }
                if (key == StatusFlowCase.SRFFx02.Key) { return StatusFlowCase.SRFFx02.Value; }
                if (key == StatusFlowCase.SRFFx03.Key) { return StatusFlowCase.SRFFx03.Value; }
                if (key == StatusFlowCase.SRFFx04.Key) { return StatusFlowCase.SRFFx04.Value; }

                if (key == StatusFlowCase.Lx01.Key) { return StatusFlowCase.Lx01.Value; }
                if (key == StatusFlowCase.Lx02.Key) { return StatusFlowCase.Lx02.Value; }
                if (key == StatusFlowCase.R8Lx01.Key) { return StatusFlowCase.R8Lx01.Value; }
                if (key == StatusFlowCase.R8Lx02.Key) { return StatusFlowCase.R8Lx02.Value; }
                if (key == StatusFlowCase.R8x01.Key) { return StatusFlowCase.R8x01.Value; }
                if (key == StatusFlowCase.R8x02.Key) { return StatusFlowCase.R8x02.Value; }
                if (key == StatusFlowCase.R8USx01.Key) { return StatusFlowCase.R8USx01.Value; }
                if (key == StatusFlowCase.R8GRCx01.Key) { return StatusFlowCase.R8GRCx01.Value; }
                if (key == StatusFlowCase.LR2x01.Key) { return StatusFlowCase.LR2x01.Value; }
                if (key == StatusFlowCase.LR2x02.Key) { return StatusFlowCase.LR2x02.Value; }

                return string.Empty;
            }

            public static bool ValidateKey(string key)
            {
                if (key == StatusFlowCase.Register.Key) { return true; }
                if (key == StatusFlowCase.D151x01.Key) { return true; }
                if (key == StatusFlowCase.D151x02.Key) { return true; }
                if (key == StatusFlowCase.D151x03.Key) { return true; }
                if (key == StatusFlowCase.D151x04.Key) { return true; }
                if (key == StatusFlowCase.USx01.Key) { return true; }
                if (key == StatusFlowCase.GRCx01.Key) { return true; }
                if (key == StatusFlowCase.GRCx02.Key) { return true; }
                if (key == StatusFlowCase.GRCx03.Key) { return true; }
                if (key == StatusFlowCase.GRCx04.Key) { return true; }
                if (key == StatusFlowCase.GRCx05.Key) { return true; }
                if (key == StatusFlowCase.MNCx01.Key) { return true; }
                if (key == StatusFlowCase.MNCx02xH.Key) { return true; }
                if (key == StatusFlowCase.MNCx02.Key) { return true; }
                if (key == StatusFlowCase.MNCx03xH.Key) { return true; }
                if (key == StatusFlowCase.MNCx03.Key) { return true; }
                if (key == StatusFlowCase.GMSx04.Key) { return true; }
                if (key == StatusFlowCase.FNx01.Key) { return true; }
                if (key == StatusFlowCase.FNx02.Key) { return true; }
                if (key == StatusFlowCase.SYSx01.Key) { return true; }
                if (key == StatusFlowCase.SYSx02.Key) { return true; }
                if (key == StatusFlowCase.SYSx03.Key) { return true; }
                if (key == StatusFlowCase.ORx01.Key) { return true; }
                if (key == StatusFlowCase.ORx02.Key) { return true; }
                if (key == StatusFlowCase.ORx03.Key) { return true; }
                if (key == StatusFlowCase.ORx04.Key) { return true; }
                if (key == StatusFlowCase.ORx05.Key) { return true; }
                if (key == StatusFlowCase.ORx06.Key) { return true; }
                if (key == StatusFlowCase.ORx07.Key) { return true; }
                if (key == StatusFlowCase.CSx01.Key) { return true; }
                if (key == StatusFlowCase.PT151x01.Key) { return true; }
                if (key == StatusFlowCase.PT151x02.Key) { return true; }
                if (key == StatusFlowCase.PT151x03.Key) { return true; }

                if (key == StatusFlowCase.RFFx01.Key) { return true; }
                if (key == StatusFlowCase.RFFx02.Key) { return true; }
                if (key == StatusFlowCase.RFFx03.Key) { return true; }
                if (key == StatusFlowCase.RFFx04.Key) { return true; }
                if (key == StatusFlowCase.RFFx05.Key) { return true; }
                if (key == StatusFlowCase.RFFx06.Key) { return true; }
                if (key == StatusFlowCase.RFFx07.Key) { return true; }

                if (key == StatusFlowCase.SRFFx01.Key) { return true; }
                if (key == StatusFlowCase.SRFFx02.Key) { return true; }
                if (key == StatusFlowCase.SRFFx03.Key) { return true; }
                if (key == StatusFlowCase.SRFFx04.Key) { return true; }

                if (key == StatusFlowCase.Lx01.Key) { return true; }
                if (key == StatusFlowCase.Lx02.Key) { return true; }
                if (key == StatusFlowCase.R8Lx01.Key) { return true; }
                if (key == StatusFlowCase.R8Lx02.Key) { return true; }
                if (key == StatusFlowCase.R8x01.Key) { return true; }
                if (key == StatusFlowCase.R8x02.Key) { return true; }
                if (key == StatusFlowCase.R8USx01.Key) { return true; }
                if (key == StatusFlowCase.R8GRCx01.Key) { return true; }
                if (key == StatusFlowCase.LR2x01.Key) { return true; }
                if (key == StatusFlowCase.LR2x02.Key) { return true; }

                return false;
            }

            public static string GetStatusWithName(string name)
            {
                if (name == StatusFlowCase.Register.Value) { return StatusFlowCase.Register.Key; }
                throw new Exception(MessageConfig.StatusNameNotFound.Value);
            }

            public static List<string> GetStatusKeyList(string name)
            {
                List<string> list = new List<string>();
                if (name == StatusFlowCase.Register.Value) { list.Add(StatusFlowCase.Register.Key); }
                if (name == StatusFlowCase.D151x01.Value) { list.Add(StatusFlowCase.D151x01.Key); }
                if (name == StatusFlowCase.D151x02.Value) { list.Add(StatusFlowCase.D151x02.Key); }
                if (name == StatusFlowCase.D151x03.Value) { list.Add(StatusFlowCase.D151x03.Key); }
                if (name == StatusFlowCase.D151x04.Value) { list.Add(StatusFlowCase.D151x04.Key); }
                if (name == StatusFlowCase.USx01.Value) { list.Add(StatusFlowCase.USx01.Key); }
                if (name == StatusFlowCase.GRCx01.Value) { list.Add(StatusFlowCase.GRCx01.Key); }
                if (name == StatusFlowCase.GRCx02.Value) { list.Add(StatusFlowCase.GRCx02.Key); }
                if (name == StatusFlowCase.GRCx03.Value) { list.Add(StatusFlowCase.GRCx03.Key); }
                if (name == StatusFlowCase.GRCx04.Value) { list.Add(StatusFlowCase.GRCx04.Key); }
                if (name == StatusFlowCase.GRCx05.Value) { list.Add(StatusFlowCase.GRCx05.Key); }
                if (name == StatusFlowCase.MNCx01.Value) { list.Add(StatusFlowCase.MNCx01.Key); }
                if (name == StatusFlowCase.MNCx02xH.Value) { list.Add(StatusFlowCase.MNCx02xH.Key); }
                if (name == StatusFlowCase.MNCx02.Value) { list.Add(StatusFlowCase.MNCx02.Key); }
                if (name == StatusFlowCase.MNCx03xH.Value) { list.Add(StatusFlowCase.MNCx03xH.Key); }
                if (name == StatusFlowCase.MNCx03.Value) { list.Add(StatusFlowCase.MNCx03.Key); }
                if (name == StatusFlowCase.GMSx04.Value) { list.Add(StatusFlowCase.GMSx04.Key); }
                if (name == StatusFlowCase.FNx01.Value) { list.Add(StatusFlowCase.FNx01.Key); }
                if (name == StatusFlowCase.FNx02.Value) { list.Add(StatusFlowCase.FNx02.Key); }
                if (name == StatusFlowCase.SYSx01.Value) { list.Add(StatusFlowCase.SYSx01.Key); }
                if (name == StatusFlowCase.SYSx02.Value) { list.Add(StatusFlowCase.SYSx02.Key); }
                if (name == StatusFlowCase.SYSx03.Value) { list.Add(StatusFlowCase.SYSx03.Key); }
                if (name == StatusFlowCase.ORx01.Value) { list.Add(StatusFlowCase.ORx01.Key); }
                if (name == StatusFlowCase.ORx02.Value) { list.Add(StatusFlowCase.ORx02.Key); }
                if (name == StatusFlowCase.ORx03.Value) { list.Add(StatusFlowCase.ORx03.Key); }
                if (name == StatusFlowCase.ORx04.Value) { list.Add(StatusFlowCase.ORx04.Key); }
                if (name == StatusFlowCase.ORx05.Value) { list.Add(StatusFlowCase.ORx05.Key); }
                if (name == StatusFlowCase.ORx06.Value) { list.Add(StatusFlowCase.ORx06.Key); }
                if (name == StatusFlowCase.ORx07.Value) { list.Add(StatusFlowCase.ORx07.Key); }
                if (name == StatusFlowCase.CSx01.Value) { list.Add(StatusFlowCase.CSx01.Key); }
                if (name == StatusFlowCase.PT151x01.Value) { list.Add(StatusFlowCase.PT151x01.Key); }
                if (name == StatusFlowCase.PT151x02.Value) { list.Add(StatusFlowCase.PT151x02.Key); }
                if (name == StatusFlowCase.PT151x03.Value) { list.Add(StatusFlowCase.PT151x03.Key); }

                if (name == StatusFlowCase.RFFx01.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.RFFx02.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.RFFx03.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.RFFx04.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.RFFx05.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.RFFx06.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.RFFx07.Value) { list.Add(StatusFlowCase.PT151x03.Key); }

                if (name == StatusFlowCase.SRFFx01.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.SRFFx02.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.SRFFx03.Value) { list.Add(StatusFlowCase.PT151x03.Key); }
                if (name == StatusFlowCase.SRFFx04.Value) { list.Add(StatusFlowCase.PT151x03.Key); }

                if (name == StatusFlowCase.Lx01.Value) { list.Add(StatusFlowCase.Lx01.Key); }
                if (name == StatusFlowCase.Lx02.Value) { list.Add(StatusFlowCase.Lx02.Key); }
                if (name == StatusFlowCase.R8Lx01.Value) { list.Add(StatusFlowCase.R8Lx01.Key); }
                if (name == StatusFlowCase.R8Lx02.Value) { list.Add(StatusFlowCase.R8Lx02.Key); }
                if (name == StatusFlowCase.R8x01.Value) { list.Add(StatusFlowCase.R8x01.Key); }
                if (name == StatusFlowCase.R8x02.Value) { list.Add(StatusFlowCase.R8x02.Key); }
                if (name == StatusFlowCase.R8USx01.Value) { list.Add(StatusFlowCase.R8USx01.Key); }
                if (name == StatusFlowCase.R8GRCx01.Value) { list.Add(StatusFlowCase.R8GRCx01.Key); }
                if (name == StatusFlowCase.LR2x01.Value) { list.Add(StatusFlowCase.LR2x01.Key); }
                if (name == StatusFlowCase.LR2x02.Value) { list.Add(StatusFlowCase.LR2x02.Key); }

                return list;
            }
        }

        #endregion

        #region | Message Config |
        public class MessageConfig : Enumeration
        {
            #region | NotFound |
            public static MessageConfig StatusKeyNotFound = new("001", "Status Key Not Found");
            public static MessageConfig StatusNameNotFound = new("002", "Status Name Not Found");
            public static MessageConfig TransClientNotFound = new("tc1", "TransClient not found");
            public static MessageConfig TranStaffNotFound = new("ts1", "TranStaff not found");
            public static MessageConfig TransCaseNotFound = new("tc1", "TransCase not found");

            public static MessageConfig AccountCreateFaild = new("ts1", "ไม่สามารถสร้างบัญชีผู้ใช้งานได้ กรุณาลองใหม่อีกครั้ง");

            public static MessageConfig AccountNotFound = new("ts1", "ไม่บัญชีผู้ใช้งาน");
            public static MessageConfig PasswordIncorrect = new("ts1", "รหัสผ่านไม่ถูกต้อง");
            public static MessageConfig TokenExpire = new("ts1", "หมดระยะเวลาการเชื่อมต่อ กรุณาลองใหม่อีกครั้ง");
            public static MessageConfig MasterMessageConfigurationNotFound = new("mmcfnf", "Please Set Master with this code : ");


            #endregion

            #region | Succeess |
            public static MessageConfig SaveSuccess = new("ss1", "Save Success");
            public static MessageConfig UpdateSuccess = new("us1", "Update Success");
            public static MessageConfig RemoveSuccess = new("rs1", "Remove Success");
            #endregion

            #region | Already |

            public static MessageConfig TransClientAlready = new("tc2", "TransClient is already");

            public static MessageConfig MasterSaleGroupAlready = new("tc2", "Sale group is already");

            public static MessageConfig ConfigKeyAlready = new("tc2", "Config Key is already");

            public static MessageConfig UsernameOfSale = new("tc2", "Username is already");

            public static MessageConfig InformationDuplicate = new("tc2", "พบข้อมูลนี้ภายในระบบอยู่แล้ว");


            #endregion

            #region | Invalid |

            public static MessageConfig ParamInvalid = new("p1", "Parameters Invalid");

            #endregion

            public MessageConfig(string Key, string Value) : base(Key, Value)
            {
            }

        }

        #endregion

        #region | Order By Type |
        public class OrderByType : Enumeration
        {
            public static OrderByType ASC = new("1", "asc");
            public static OrderByType DESC = new("2", "desc");
            public OrderByType(string Key, string Value) : base(Key, Value)
            {
            }

        }

        #endregion

        #region | Paymennt Channel |
        public class PaymentChannelType : Enumeration
        {
            public static PaymentChannelType Cash = new("CH01", "เงินสด");
            public static PaymentChannelType CreditCard = new("CH03", "บัตรเครดิต");
            public static PaymentChannelType QRCode = new("CH02", "QR Code");
            public PaymentChannelType(string Key, string Value) : base(Key, Value)
            {
            }

            public static string GetKey(string value)
            {
                if (value == PaymentChannelType.Cash.Value) { return PaymentChannelType.Cash.Key; }
                if (value == PaymentChannelType.CreditCard.Value) { return PaymentChannelType.CreditCard.Key; }
                if (value == PaymentChannelType.QRCode.Value) { return PaymentChannelType.QRCode.Key; }
                throw new Exception(TextFix.TextValueInvalid);
            }

            public static string GetValue(string value)
            {
                if (value == PaymentChannelType.Cash.Key) { return PaymentChannelType.Cash.Value; }
                if (value == PaymentChannelType.CreditCard.Key) { return PaymentChannelType.CreditCard.Value; }
                if (value == PaymentChannelType.QRCode.Key) { return PaymentChannelType.QRCode.Value; }
                throw new Exception(TextFix.TextKeyInvalid);
            }
        }
        #endregion

        #region | Role |
        public class RoleConfig : Enumeration
        {
            public static RoleConfig Finance = new("finance", "การเงิน");
            public static RoleConfig Manager = new("manager", "ผู้จัดการ");
            public static RoleConfig Ultrasound = new("ultrasound", "Ultrasound");
            public static RoleConfig Executive = new("executive", "ผู้บริหาร");
            public static RoleConfig User = new("user", "ผู้ใชงาน - คนไข้");
            public static RoleConfig ManagerConsult = new("manager_consult", "ผู้จัดการ Consult");
            public static RoleConfig Development = new("development", "ฝ่ายพัฒนาธุรกิจ");
            public static RoleConfig Stat = new("stat", "ฝ่ายสถิติ");
            public static RoleConfig Sale = new("sale", "พนักงานขาย");
            public static RoleConfig Consult = new("consult", "เจ้าหน้าที่ Consult");
            public static RoleConfig Counter = new("counter", "Counter 151");
            public static RoleConfig Administrator = new("administrator", "ผู้ดูแลระบบ");
            public static RoleConfig AssistantManagerConsult = new("assistant_manager_consult", "ผู้ช่วยผู้จัดการ Consult");
            public static RoleConfig R8 = new("r8", "R8");
            public static RoleConfig LR2 = new("lr2", "LR2");

            public RoleConfig(string Key, string Value) : base(Key, Value)
            {
            }

            public static string GetKey(string value)
            {
                if (value == RoleConfig.Finance.Value) { return RoleConfig.Finance.Key; }
                if (value == RoleConfig.Manager.Value) { return RoleConfig.Manager.Key; }
                if (value == RoleConfig.Ultrasound.Value) { return RoleConfig.Ultrasound.Key; }
                if (value == RoleConfig.Executive.Value) { return RoleConfig.Executive.Key; }
                if (value == RoleConfig.User.Value) { return RoleConfig.User.Key; }
                if (value == RoleConfig.ManagerConsult.Value) { return RoleConfig.ManagerConsult.Key; }
                if (value == RoleConfig.Development.Value) { return RoleConfig.Development.Key; }
                if (value == RoleConfig.Stat.Value) { return RoleConfig.Stat.Key; }
                if (value == RoleConfig.Sale.Value) { return RoleConfig.Sale.Key; }
                if (value == RoleConfig.Consult.Value) { return RoleConfig.Consult.Key; }
                if (value == RoleConfig.Counter.Value) { return RoleConfig.Counter.Key; }
                if (value == RoleConfig.Administrator.Value) { return RoleConfig.Administrator.Key; }
                if (value == RoleConfig.AssistantManagerConsult.Value) { return RoleConfig.AssistantManagerConsult.Key; }
                if (value == RoleConfig.R8.Value) { return RoleConfig.R8.Key; }
                if (value == RoleConfig.LR2.Value) { return RoleConfig.LR2.Key; }
                return string.Empty;
            }

            public static string GetValue(string value)
            {
                if (value == RoleConfig.Finance.Key) { return RoleConfig.Finance.Value; }
                if (value == RoleConfig.Manager.Key) { return RoleConfig.Manager.Value; }
                if (value == RoleConfig.Ultrasound.Key) { return RoleConfig.Ultrasound.Value; }
                if (value == RoleConfig.Executive.Key) { return RoleConfig.Executive.Value; }
                if (value == RoleConfig.User.Key) { return RoleConfig.User.Value; }
                if (value == RoleConfig.ManagerConsult.Key) { return RoleConfig.ManagerConsult.Value; }
                if (value == RoleConfig.Development.Key) { return RoleConfig.Development.Value; }
                if (value == RoleConfig.Stat.Key) { return RoleConfig.Stat.Value; }
                if (value == RoleConfig.Sale.Key) { return RoleConfig.Sale.Value; }
                if (value == RoleConfig.Consult.Key) { return RoleConfig.Consult.Value; }
                if (value == RoleConfig.Counter.Key) { return RoleConfig.Counter.Value; }
                if (value == RoleConfig.Administrator.Key) { return RoleConfig.Administrator.Value; }
                if (value == RoleConfig.AssistantManagerConsult.Key) { return RoleConfig.AssistantManagerConsult.Value; }
                if (value == RoleConfig.R8.Key) { return RoleConfig.R8.Value; }
                if (value == RoleConfig.LR2.Key) { return RoleConfig.LR2.Value; }
                return string.Empty;
            }
        }
        #endregion

        #region | Bucket Type |

        public class BucketType : Enumeration
        {
            public BucketType(string Key, string Value) : base(Key, Value)
            {
            }

            public static BucketType Hospital = new("1", "โรงพยาบาล");
            public static BucketType Association = new("2", "สมาคม");
            public static BucketType ReserveFunds = new("3", "สำรองจ่าย");

            public static string GetValue(string key)
            {
                if (key == BucketType.Hospital.Key) { return BucketType.Hospital.Value; }
                if (key == BucketType.Association.Key) { return BucketType.Association.Value; }
                if (key == BucketType.ReserveFunds.Key) { return BucketType.ReserveFunds.Value; }
                return string.Empty;
            }
        }

        #endregion

        #region | FixNumberText |
        public static class FixNumberText
        {
            public static string Number1 = "1";
            public static string Number2 = "2";
            public static string Number3 = "3";
            public static string Number4 = "4";
            public static string Number5 = "5";
            public static string Number6 = "6";
            public static string Number7 = "7";
            public static string Number8 = "8";
            public static string Number9 = "9";
            public static string Number0 = "0";
        }
        #endregion

        #region | Text Group Config System |
        public static class TextGroupConfigSystem
        {
            public static string GroupGenerate = "GroupGenerate";
        }
        #endregion

        #region | Text KEY Config System |
        public static class TextKeyConfigSystem
        {
            public static string NoOtp = "no-otp";

            public static string DisableCaseAuto = "DisableCaseAuto";

            public static string FinishCaseHour = "FinishCaseHour";

            public static string RevokeRefCode = "RevokeRefCode";

        }
        #endregion

        #region | Format Date Time |

        public static class FormatDateTimeString
        {
            public static string FormatddMMyyy = "ddMMyyy";
            public static string FormatyyyyMMdd = "yyyyMMdd";
            public static string FormatyyMMdd = "yyMMdd";
            public static string FormatddSlashMMslashYYYY = "dd/MM/yyyy";
            public static string FormatddSlashMMslashYYYYHHmm = "dd/MM/yyyy HH:mm";
            public static string FormatddMMyyyyHHmmss = "ddMMyyyyHHmmss";
        }


        #endregion

        #region | Text Fix |

        public static class TextFix
        {
            public static string ConsultGeneral = "general";
            public static string ConsultManager = "manager";

            public static string TextTransClient = "TransClient";
            public static string TextTransCase = "TransCase";
            public static string TextTransPayment = "TransPayment";
            public static string TextTransOrder = "TransOrder";
            public static string TextTransReferralSummary = "TransReferralSummary";
            public static string TextSystem = "System";
            public static string TextKeyMasterMessageConfiguration = "MasterMessageConfiguration";

            public static string TextBucketTypeHospital = "BucketTypeHospital";
            public static string TextBucketTypeAssociation = "BucketTypeAssociation";
            public static string TextKeyInvalid = "Key invalid";
            public static string TextValueInvalid = "Value invalid";

            public static string TextKeyMXR = "MXR";
            public static string TextTransPaymentRefund = "TransPaymentRefund";

            public static string TextNewAppointment = "นัดหมายใหม่";
            public static string TextFreeUs = "Free U/S";
            public static string TextFreePt = "Free P/T";

            public static string TextWaitingForAppointmentDate = "รอวันนัดหมาย";
            public static string TextUnWaitingForAppointmentDate = "ไม่มาตามนัด";
            public static string TextRSA = "rsa";

            public static string TextApprovedTh = "อนุมัติแล้ว";

            #region | System |

            public static string TextHospital = "โรงพยาบาล";
            public static string TextAssociation = "สมาคม";

            public static string TextHospitalEn = "hospital";
            public static string TextAssociationEn = "association";


            #endregion

            #region | Master Order Item Group |
            public static string TextMedicine = "Medicine";
            public static string TextSleep = "Sleep";
            #endregion

            #region | Payment Type |
            public static string PaymentTypeReceive = "Receive";
            public static string PaymentTypeRefund = "Refund";

            public static string PaymentTypeReceiveTh = "รับเงิน";
            public static string PaymentTypeRefundTh = "คืนเงิน";
            #endregion

            #region | Config Sys Configuration |
            /// <summary>
            /// Role Code Can Create Order
            /// </summary>
            public static string TextKeyConfigRCCO = "RCCO";
            /// <summary>
            /// Config Role can approve trans order
            /// </summary>
            public static string TextKeyConfigAPOR = "APOR";
            /// <summary>
            /// config amount default referral fee
            /// </summary>
            public static string TextMaxReferralAmount = "MaxReferralAmount";
            /// <summary>
            /// config default referral
            /// </summary>
            public static string TextDefaultReferral = "DefaultReferral";
            /// <summary>
            /// config min amount default referral fee
            /// </summary>
            public static string TextMinReferralAmount = "MinReferralAmount";
            /// <summary>
            /// config Running Auto Consult Room
            /// </summary>
            public static string TextKeyRCR = "RCR";
            /// <summary>
            /// config Max Consult Room
            /// </summary>
            public static string TextKeyMCR = "MCR";
            /// <summary>
            /// config Path upload file
            /// </summary>
            public static string TextKeyPULF = "PULF";

            public static string TextKeyDomain = "DOMAIN";

            #endregion

            #region | Master Table Name |
            public static string MasterConsultRoom = "masterConsultRoom";
            public static string MasterCountry = "masterCountry";
            public static string MasterGestationalAge = "masterGestationalAge";
            public static string MasterHospital = "masterHospital";
            public static string MasterItemsOrder = "masterItemsOrder";
            public static string MasterMessageConfiguration = "masterMessageConfiguration";
            public static string MasterNationality = "masterNationality";
            public static string MasterPhysician = "masterPhysician";
            public static string MasterReasonNotTreatment = "masterReasonNotTreatment";
            public static string MasterRightTreatment = "masterRightTreatment";
            public static string MasterSaleGroup = "masterSaleGroup";
            public static string MasterStatus = "masterStatus";
            public static string MasterThaiDistricts = "masterThaiDistricts";
            public static string MasterThaiProvinces = "masterThaiProvinces";
            public static string MasterThaiSubdistricts = "masterThaiSubdistricts";
            public static string MasterPaymentChannel = "masterPaymentChannel";
            public static string MasterReferralFrom = "masterReferralFrom";
            public static string MasterReasonNewAppointment = "masterReasonNewAppointment";
            public static string MasterReasonUnFollow = "masterReasonUnFollow";
            public static string MasterChannelInformation = "masterChannelInformation";


            #endregion

            #region | Fix Code Master Message Configuration |

            public static string TextCodeAlready = "ALREADY";
            public static string TextCodeSaveFailed = "SAVE_FAILD";
            public static string TextCodeSaveSuccess = "SAVE_SUCCESS";
            public static string TextCodeDataNotFound = "DATA_NOT_FOUND";

            public static string TextCodeTelephoneAlready = "TELEPHONE_IS_ALREADY";
            public static string TextCodeTelephoneKeyNew = "TELEPHONE_KEY_NEW";

            public static string TextCodeLoginSuccess = "LOGIN_SUCCESS";
            public static string TextCodeSearchSuccess = "SEARCH_SUCCESS";
            public static string TextCodeTransClientNotFound = "TRANS_CLIENT_NOT_FOUND";
            public static string TextCodeUpdateSuccess = "UPDATE_SUCCESS";
            public static string TextCodeUpdateFailed = "UPDATE_FAILED";
            public static string TextCodeDeletedFailed = "DELETED_FAILED";

            public static string TextCodeDeletedSuccess = "DELETE_SUCCESS";
            public static string TextCodePasswordIncorrect = "PASSWORD_INCORRECT";
            public static string TextCodeAccountNotFound = "ACCOUNT_NOT_FOUND";
            public static string TextCodeRefCodeNotFound = "REF_CODE_NOT_FOUND";
            public static string TextCodePhoneCodeNotFound = "PHONE_CODE_NOT_FOUND";
            public static string TextCodeTelephoneNotFound = "TELEPHONE_PHONE_NOT_FOUND";

            public static string TextCodeRoleNotFound = "ROLE_NOT_FOUND";

            public static string TextCodeStatusNotFound = "STATUS_NOT_FOUND";
            public static string TextCodeMasterItemNotFound = "MASTER_ITEM_NOT_FOUND";
            public static string TextCodeRemarkSpecialApproveRequired = "REMARK_SPECIAL_APPROVE_REQUIRED";
            public static string TextCodeDiscountInvalid = "DISCOUNT_INVALID";
            public static string TextCodeCaseNotFound = "CASE_NOT_FOUND";

            public static string TextCodeItemInvalid = "ITEM_INVALID";
            public static string TextCodeDrugAllergyRemarkIsRequired = "DRUG_ALLERGY_REMARK_IS_REQUIRED";
            public static string TextCodeAmountInvalid = "AMOUNT_INVALID";
            public static string TextCodeTransOrderItemNotFound = "TRANS_ORDER_ITEM_NOT_FOUND";

            public static string TextCodeCongenitalDiseaseRemarkIsRequired = "CONGENITAL_DISEASE_REMARK_IS_REQUIRED";
            public static string TextCodeConsultRoomNotActive = "CONSULT_ROOM_DISABLE";
            public static string TextCodeConsultRoomInvalid = "CONSULT_ROOM_INVALID";
            public static string TextCodeOrderItemDuplicate = "ORDER_ITEM_DUPLICATE";

            public static string TextCodeConsultRoomIsAlready = "CONSULT_ROOM_IS_ALREADY_IN_ROOM";
            public static string TextCodeConsultRoomIsAlready1 = "CONSULT_ROOM_IS_ALREADY_IN_ROOM_1";
            public static string TextCodeConsultRoomIsAlready2 = "CONSULT_ROOM_IS_ALREADY_IN_ROOM_2";
            public static string TextCodeConsultRoomIsAlready3 = "CONSULT_ROOM_IS_ALREADY_IN_ROOM_3";
            public static string TextCodeConsultRoomIsAlready4 = "CONSULT_ROOM_IS_ALREADY_IN_ROOM_4";


            public static string TextCodeMoveConsultRoom1 = "MOVE_TO_CONSULT_ROOM_1";
            public static string TextCodeMoveConsultRoom2 = "MOVE_TO_CONSULT_ROOM_2";
            public static string TextCodeMoveConsultRoom3 = "MOVE_TO_CONSULT_ROOM_3";
            public static string TextCodeMoveConsultRoom4 = "MOVE_TO_CONSULT_ROOM_4";

            public static string TextCodePaymentChannelInvalid = "PAYMENT_CHANNEL_INVALID";
            public static string TextCodeRequiredPaymentChannelCard = "REQUIRED_PAYMENT_CHANNEL_CARD";
            public static string TextCodeConfigInvalid = "CONFIG_INVALID";
            public static string TextCodeRoleNoPerssionCreateOrder = "ROLE_NO_PERMISSION_CREATE_ORDER";

            public static string TextCodeTransCaseNotFound = "TRANS_CASE_NOT_FOUND";
            public static string TextCodeStatusCantUpdate = "STATUS_CANT_UPDATE";
            public static string TextCodeTransOrderNotFound = "TRANS_ORDER_NOT_FOUND";
            public static string TextCodeMoneyInvalid = "MONEY_INVALID";
            public static string TextCodeOrderItemPaySuccess = "ORDER_ITEM_PAY_SUCCESS";

            public static string TextCodeTransCaseCannotSave = "TRANS_CASE_CAN_NOT_SAVE";
            public static string TextCodeTransCaseInProgress = "TRANS_CASE_IN_PROGRESS";

            public static string TextCodeClientNotMatchCase = "CLIENT_NOT_MATCH_CASE";
            public static string TextCodeTransPaymentNotFound = "TRANS_PAYMENT_NOT_FOUND";
            public static string TextCodeCantRefund = "CANT_REFUND";
            public static string TextCodeCantMoveMoneyBucket = "CANT_MOVE_MONEY_BUCKET";
            public static string TextCodeTransStaffNotFound = "TRANS_STAFFNOT_FOUND";

            public static string TextCodeSaleNotFound = "SALE_NOT_FOUND";
            public static string TextCodeStartDateMoreEndDate = "SDATE_MORE_EDATE";

            public static string TextCodePasswordNotMatch = "PASSWORD_NOT_MATCH";
            public static string TextCodeMasterReasonUnfollowNotFound = "MASTER_REASON_UNFOLLOW_NOT_FOUND";
            public static string TextCodeDateMoreDefault = "DATE_MORE_DEFAULT";

            public static string TextCodeAssistManagerAlready = "ASSIST_MANAGER_ALREADY";

            public static string TextCodeMoneyBucketInvalid = "MONEY_BUCKET_INVALID";

            public static string TextCodeTransCaseAlready = "TRANSCASE_ALREADY";
            public static string TextFileNotFound = "FILE_NOT_FOUND";

            #endregion


        }

        #endregion

        #region | Text Yes/ No |
        public class TextYesNo : Enumeration
        {
            public TextYesNo(string Key, string Value) : base(Key, Value)
            {
            }
            public static TextYesNo Yes = new("1", "Yes");
            public static TextYesNo No = new("0", "No");
        }
        #endregion


        public static class MasterSaleGroupCode
        {
            /// <summary>
            /// ทีมขายโรงพยาบาลคลองตัน
            /// </summary>
            public static string Code0001 = "0001";
            /// <summary>
            ///ทีม RSA
            /// </summary>
            public static string Code0002 = "0002";
            /// <summary>
            /// Jenny
            /// </summary>
            public static string Code0003 = "0003";
            /// <summary>
            /// ทำทาง
            /// </summary>
            public static string Code0004 = "0004";
            /// <summary>
            /// Art
            /// </summary>
            public static string Code0005 = "0005";
            /// <summary>
            /// ฝ่าย Operation โรงพยาบาลคลองตัน
            /// </summary>
            public static string Code0006 = "0006";
            /// <summary>
            /// ทีมสุนิสา
            /// </summary>
            public static string Code0007 = "0007";
        }

        public static class MasterReasonNewAppointmentCode
        {
            public static string Code00001 = "00001"; //แฟนมาเซ็น
            public static string Code00002 = "00002"; //ค่าใช้จ่ายไม่พอ
            public static string Code00003 = "00003"; //เดี๋ยวมา
            public static string Code00004 = "00004"; //นัด Follow
            public static string Code00005 = "00005"; //ให้ผู้ปกครองมาเซ็น
            public static string Code00006 = "00006"; //ติดธุระ
            public static string Code00007 = "00007"; //ขอปรึกษาญาติ
            public static string Code00008 = "00008"; //อื่นๆ
            public static string Code00009 = "00009"; //ติดค้างคืน
            public static string Code00010 = "00010"; //นัดพบแพทย์
            public static string Code00011 = "00011"; //U/S ซ้ำ
            public static string Code00012 = "00012"; //มาสอบถามราคา
        }

        public static class MasterReasonNewAppointmentGroup
        {
            public static string Follow = "FOLLOW";
            public static string Comeback = "COMEBACK";
        }

        public static class MasterItemOrderGroupCode
        {
            /// <summary>
            ///ห้องพักธรรมดา
            /// </summary>
            public static string Code0001 = "0001";
            /// <summary>
            ///ห้องพักพิเศษ
            /// </summary>
            public static string Code0002 = "0002";
            /// <summary>
            ///ทั่วไป
            /// </summary>
            public static string Code0003 = "0003";
            /// <summary>
            ///ทำเลย
            /// </summary>
            public static string Code0004 = "0004";
            /// <summary>
            /// ทานยา
            /// </summary>
            public static string Code0005 = "0005";
            /// <summary>
            /// ฟรี
            /// </summary>
            public static string Code0006 = "0006";
        }

        public static class MasterGestationalAgeGroup
        {
            public static string GroupM = "m";
            public static string GroupP = "p";
            public static string GroupH = "h";
        }

        public static class MasterStatusFilterPaymentAndRefund
        {
            public static string StatusNormal = "normal";
            public static string StatusCancel = "cancel";
        }

        public static class OrderType
        {
            public static string Us = "us";
            public static string Pt = "pt";
            public static string Normal = "normal";
        }

        public static class ConfigGuid
        {
            public static class MasterItemOrderGroup
            {
                public static Guid TakeMedication = new Guid("1f4d0d58-e73e-43d9-a568-9bc4063269d3");
                public static Guid DoItNow = new Guid("85c10777-ce5a-4a35-8a8f-22ec6e420b5d");
            }
        }

        public static class TextAction
        {
            public static string Approve = "Approve";
            public static string Cancel = "Cancel";
        }

    }

}

