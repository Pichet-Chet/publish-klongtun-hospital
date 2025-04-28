export type CaseDetailFromProps = {
  caseData?: TransCaseProps;
  clientData?: TransClientProps;
};

export type CaseInfoTabContentFromProps = {
  id?: string;
  transCaseId?: string;
  masterGestationalAgeId?: string;
  masterReferralFromId?: string;
  drugAllergy?: boolean;
  drugAllergyRemark?: string;
  congenitalDisease?: boolean;
  congenitalDiseaseRemark?: string;
  caesareanSection?: boolean;
  relatives?: boolean;
  patient?: boolean;
  createdBy?: string;
  createdDate?: Date;
  withdraw?: boolean | string;
  updatedBy?: string;
  updatedDate?: Date;
  comment?: string;
  comments?: TransCaseCommentProps[];
  onAction?: "" | "comment" | "update";
};

export type TransCaseCommentProps = {
  description?: string;
  updatedDate?: Date;
  updatedBy?: string;
};

export type CaseInfoTabActionFromProps = {
  transCaseId?: string;

  cancelMasterReasonNotTreatmentId?: string;
  cancelRemark?: string;
  cancelCreatedBy?: string;

  reopenNewAppointment?: Date;
  reopenRemark?: string;
  reopenIsFreeUs?: boolean | string;
  reopenCreatedBy?: string;

  sendToManagerRemark?: string;

  onAction?: "" | "cancel" | "reopen" | "toManager";
};

export type AssignConsultRoomResponseProps = {
  masterConsultRoomId?: string;
  masterConsultRoomName?: string;
};
