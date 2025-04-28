export type ClientListFromProps = {
  textSearch?: string;
  clients?: ClientProps[];
};

export type ClientProps = {
  textSearch?: string;
  id?: string;
  fullName?: string;
  citizenIdentification?: string;
  passportNumber?: string;
  address?: string;
  dateOfBirth?: string;
  telephoneNumber?: string;
  masterNationalityId?: string;
  isActive: true;
  createdBy?: string;
  createdDate?: string;
  updatedBy?: string;
  updatedDate?: string;
  telephoneCode?: string;
  telephoneSecond?: string;
  isDelete?: boolean;
  deletedDate?: string;
  deletedBy?: string;
  masterRightTreatmentId?: string;
  masterHospitalId?: string;
  tranSaleRefCode?: string;
  hnNo?: string;
  clientNo?: string;
  occupation?: string;
};
