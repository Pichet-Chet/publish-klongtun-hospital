export type AllMasterProps = {
  masterName: string;
  items: any[];
};

export type MasterCountryProps = {
  id: string;
  nameEn: string;
  nameTh: string;
  isActive: boolean;
  code: string;
  flag: string;
  languageCode: string;
  telephoneCode: string;
};

export type MasterNationalityProps = {
  id: string;
  nameEn: string;
  nameTh: string;
  isActive: boolean;
};

export type MasterRightTreatmentProps = {
  id: string;
  name: string;
  isActive: boolean;
};

export type MasterGestationalAgeProps = {
  id: string;
  name: string;
  isActive: boolean;
};

export type MasterReferralFromProps = {
  id: string;
  name: string;
  isActive: boolean;
};

export type MasterReasonNotTreatmentProps = {
  id: string;
  name: string;
  isActive: boolean;
};
