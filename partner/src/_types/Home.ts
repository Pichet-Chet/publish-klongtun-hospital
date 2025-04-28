export type HomeFromProps = {
  textSearch?: string;
  cases?: TransCaseProps[];
  appointment?: AppointmentProps;
  caseForSale?: CaseForSale;
  countCase?: CountCase;
  selectCreateDate?: [Date | null, Date | null];
  masterReasonUnfollow?: MasterReasonUnFollow[];
  unfollowRequest?: UnfollowRequest | null;
  updateSaleRemarkRequest?: UpdateRemarkSaleRequest | null;
  updateReceiveServiceDateRequest?: UpdateReceiveServiceDateRequest | null;
  findWithPhoneRequest?: FindWithPhoneRequest | undefined;
  createTransClientRequest?: CreateTransClientRequest
  masterRefCode?: MasterRefCode[];
};

export type AppointmentProps = {
  appointment?: number;
  appointmentPercent?: string;
  walkin?: number;
  walkinPercent?: string;
  register?: number;
  registerPercent?: string;
  all?: number;
  allPercent?: string;
};

export type CaseForSale = {
  countCaseFilter: number;
  urlSale: string;
  caseDetailData?: CaseDetail[];
};

export type CountCase = {
  countCaseYear: number;
  countCaseYearBySale: number;
  countCaseCureMonth: number;
  countCaseCureMonthBySale: number;
  countContract: string[];
  countHealing: string[];
  maxValueChart: number;
};

export type CaseDetail = {
  id: string,
  receiveServiceDate: string,
  fullName: string,
  refCode: string,
  statusName: string,
  transCaseNo: string,
  createDate: string,
  clientTel: string,
  saleRecord: string,
  isRsa: boolean,
  saleFullName: string,
  updateDate: string,
  transSaleId: string
}

export type MasterReasonUnFollow = {
  id: string,
  name: string,
  isActive: boolean,
  createdBy: string,
  createdDate: Date,
  updatedBy: string,
  updatedDate: Date,
}

export type UnfollowRequest = {
  caseId: string,
  masterUnFollowId: string,
  remarkUnfollow: string
}

export type UnfollowResponse = {
  id: string
}

export type UpdateRemarkSaleRequest = {
  caseId: string,
  remark: string
}

export type UpdateRemarkSaleResponse = {
  id: string
}

export type UpdateReceiveServiceDateRequest = {
  caseId: string,
  receiveServiceDate: Date
}

export type UpdateReceiveServiceDateResponse = {
  id: string
}

export type FindWithPhoneRequest = {
  code: string
  phone?: string
}

export type FindWithPhoneResponse = {
  isDuplicate?: boolean;
  dateOfBirth?: Date;
  fullName?: string;
};

export type CreateTransClientRequest = {
  fullName?: string
  dateOfBirth?: Date
  telephoneNumber?: string
  tranSaleRefCode?: string
  gestationalAge?: number
  saleRecord?: string
  receiveServiceDate?: Date
}

export type MasterRefCode = {
  id: string,
  username: string,
  fullName: string,
  nickName: string,
  isActive: boolean,
  createdBy: string,
  createdDate: Date,
  updatedBy: string,
  updatedDate: Date,
  masterSaleGroup?: string
  refCode: string
}