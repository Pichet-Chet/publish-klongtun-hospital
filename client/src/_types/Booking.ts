export type BookingUserRequestFormProps = {
  fullName?: string;
  citizenIdentification?: string;
  address?: string;
  dateOfBirth?: Date | null;
  telephoneNumber?: string;
  masterNationalityId?: string;
  lastMonthlyPeriod?: Date; //ประจำเดือนครั้งล่าสุด
  gestationalAge?: number; //อายุครรภ์
  historyOfCesareanSection?: string; //ประวัติการผ่าคลอด
  marriedOrBoyfriend?: string; //การแต่งงาน / มีแฟน
  drugAllergy?: string; //ประวัติการแพ้ยา
  congenitalDisease?: string; //โรคประจำตัว
  reasonTermination?: string; //เหตุผลการยุติฯ
  informationToDoctor?: string; //ข้อมูลเพิ่มเติมถึงแพทย์
  receiveServiceDate?: Date; //วันที่ที่ต้องการเข้ารับบริการ
};
export type BookingUserRequestApiProps = {
  lastMonthlyPeriod?: string; //ประจำเดือนครั้งล่าสุด
  gestationalAge?: number; //อายุครรภ์
  historyOfCesareanSection?: boolean; //ประวัติการผ่าคลอด
  marriedOrBoyfriend?: boolean; //การแต่งงาน / มีแฟน
  drugAllergy?: string; //ประวัติการแพ้ยา
  congenitalDisease?: string; //โรคประจำตัว
  reasonTermination?: string; //เหตุผลการยุติฯ
  informationToDoctor?: string; //ข้อมูลเพิ่มเติมถึงแพทย์
  receiveServiceDate?: string; //วันที่ที่ต้องการเข้ารับบริการ
  transClientId?: string; //เจ้าของงาน
  isActive?: boolean;
  createdBy?: string;
  masterStatusCode?: string;
  masterConsultRoomId?: string;
};
