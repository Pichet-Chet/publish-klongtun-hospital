export type AuthProfileProps = {
  id?: string;
  fullName?: string;
  telephoneCode?: string;
  telephoneNumber?: string;
  tranSaleRefCode?: string;
  dateOfBirth?: Date;
};

export type UserHaveCaesProps = {
  caseId?: string;
  caseIsActive?: boolean;
  receiveServiceDate?: Date;
  isNewAppointment?: boolean;
};

export type TransClientProps = {
  id?: string | null;
  fullName?: string | null;
  citizenIdentification?: string | null;
  passportNumber?: string | null;
  address?: string | null;
  dateOfBirth?: Date | string | null;
  telephoneNumber?: string | null;
  masterNationalityId?: string | null;
  isActive?: boolean;
  createdBy?: string;
  createdDate?: Date;
  updatedBy?: string;
  updatedDate?: Date;
  telephoneCode?: string;
  telephoneSecond?: string;
  masterRightTreatmentId?: string;
  tranSaleRefCode?: string;
  hnNo?: string;
  clientNo?: string;
  occupation?: string;
  hostpitalName?: string;
};
