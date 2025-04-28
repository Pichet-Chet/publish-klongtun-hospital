export type RegisterUserFromProps = {
  telephoneNumber?: string;
  dateOfBirth?: Date;
};

export type VerifyMobilePhoneAndDateOfBirthProps = {
  isData?: boolean;
};

export type FindWithPhoneProps = {
  isDuplicate?: boolean;
};

export type OtpThailandProps = {
  code?: string;
  detail?: string;
  result?: ResultOtpThailand;
};
export type ResultOtpThailand = {
  phone?: string;
  token?: string;
  otp_code?: string;
  ref_code?: string;
};

export type RegisterUserRequestFormProps = {
  tranSaleRefCode?: string; //ผู้แนะนำ
  fullName?: string; //ชื่อ - นามสกุล
  citizenIdentification?: string; //บัตรประชาชน TH
  passportNumber?: string; //บัตรประชาชน EN
  address?: string; //ที่อยู่
  dateOfBirth?: Date; //วันเกิด
  telephoneCode?: string;
  telephoneNumber?: string; //เบอร์โทรศัพท์มือถือ
  masterNationality?: string; //สัญชาติ
  lastMonthlyPeriod?: Date; //ประจำเดือนครั้งล่าสุด
  gestationalAge?: number; //อายุครรภ์
  historyOfCesareanSection?: string; //ประวัติการผ่าคลอด
  marriedOrBoyfriend?: string; //การแต่งงาน / มีแฟน
  drugAllergy?: string; //ประวัติการแพ้ยา
  congenitalDisease?: string; //โรคประจำตัว
  reasonTermination?: string; //เหตุผลการยุติฯ
  informationToDoctor?: string; //ข้อมูลเพิ่มเติมถึงแพทย์
  receiveServiceDate?: Date; //วันที่ที่ต้องการเข้ารับบริการ
  createdBy?: string; //ชื่อ - นามสกุล
};
export type RegisterUserRequestApiProps = {
  tranSaleRefCode?: string; //ผู้แนะนำ
  fullName?: string; //ชื่อ - นามสกุล
  citizenIdentification?: string; //บัตรประชาชน TH
  passportNumber?: string; //บัตรประชาชน EN
  address?: string; //ที่อยู่
  dateOfBirth?: string; //วันเกิด
  telephoneCode?: string;
  telephoneNumber?: string; //เบอร์โทรศัพท์มือถือ
  masterNationalityId?: string; //สัญชาติ
  lastMonthlyPeriod?: string; //ประจำเดือนครั้งล่าสุด
  gestationalAge?: number; //อายุครรภ์
  historyOfCesareanSection?: boolean; //ประวัติการผ่าคลอด
  marriedOrBoyfriend?: boolean; //การแต่งงาน / มีแฟน
  drugAllergy?: string; //ประวัติการแพ้ยา
  congenitalDisease?: string; //โรคประจำตัว
  reasonTermination?: string; //เหตุผลการยุติฯ
  informationToDoctor?: string; //ข้อมูลเพิ่มเติมถึงแพทย์
  receiveServiceDate?: string; //วันที่ที่ต้องการเข้ารับบริการ
  createdBy?: string; //ชื่อ - นามสกุล
};
