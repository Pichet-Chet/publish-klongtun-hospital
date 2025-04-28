export type AppointmentRequestFormProps = {
  viewReceiveServiceDate?: Date;
  appointmentData?: Date;
};

export type CancelAppointmentRequestApiProps = {
  id?: string;
  statusCode?: "CS-01";
};

export type TransCaseApiProps = {
  id?: string;
  lastMonthlyPeriod?: Date;
  gestationalAge?: number;
  historyOfCesareanSection?: boolean;
  marriedOrBoyfriend?: boolean;
  drugAllergy?: string;
  congenitalDisease?: string;
  reasonTermination?: string;
  informationToDoctor?: string;
  saleRecord?: string;
  receiveServiceDate?: Date | string;
  isActive?: boolean;
  transClientId?: string;
  caseNo?: string;
  masterStatusCode?: string;
  masterConsultRoomId?: string;
  updatedBy?: string;
};
