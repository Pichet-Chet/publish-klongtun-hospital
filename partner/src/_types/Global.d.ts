declare global {
  interface OptionProps {
    value: string;
    label: string;
  }

  interface OptionMultiLanguageProps {
    value: string;
    labelTh: string;
    labelEn: string;
  }

  interface ResponseProps {
    data?: DataResponseProps;
  }

  interface ResponseErrorProps {
    status?: number;
    response?: DataResponseErrorProps;
  }

  interface TransCaseProps {
    id?: string;
    caseNo?: string;
    lastMonthlyPeriod?: Date;
    gestationalAge?: number;
    historyOfCesareanSection?: boolean;
    marriedOrBoyfriend?: boolean;
    drugAllergy?: string;
    congenitalDisease?: string;
    reasonTermination?: string;
    informationToDoctor?: string;
    saleRecord?: string;
    receiveServiceDate?: Date;
    isActive?: boolean;
    transClientId?: string;
    masterStatusCode?: MasterStatusCode;
    masterStatusName?: string;
    remark?: string;
    howManyTimes?: string;
    numberOfChildren?: string;
    masterConsultRoomId?: string;
    masterConsultRoomName?: string;
    createdBy?: string;
    createdDate?: Date;
    updatedBy?: string;
    updatedDate?: Date;
    transClientData?: TransCaseClientProps;
  }
  interface TransClientProps {
    id?: string;
    fullName?: string;
    citizenIdentification?: string;
    passportNumber?: string;
    address?: string;
    dateOfBirth?: Date | string;
    telephoneNumber?: string;
    masterNationalityId?: string;
    accessToken?: string;
    accessTokenExpire?: Date;
    isActive?: boolean;
    createdBy?: string;
    createdDate?: Date;
    updatedBy?: string;
    updatedDate?: Date;
    telephoneCode?: string;
    telephoneSecond?: string;
    isDelete?: boolean;
    deletedDate?: Date;
    deletedBy?: string;
    masterRightTreatmentId?: string;
    masterHospitalId?: string;
    tranSaleRefCode?: string;
    hnNo?: string;
    clientNo?: string;
    occupation?: string;
    transClientHeader?: TransCaseClientProps;
  }
  interface TransCaseClientProps {
    clientNo?: string;
    fullname?: string;
    hnNo?: string;
    age?: string;
    saleName?: string;
    saleGroup?: string;
  }

  interface FinancePaymentProps {
    id?: string;
    transactionNo?: string;
    transactionDate?: Date;
    totalAmount?: number;
    remark?: string;
    createdBy?: string;
    isReceipt: true;
    transCaseNo?: string;
    transCaseId?: string;
    pos?: string;
    paymentChannel?: string;
    transPaymentItem: FinancePaymentItemProps[];
  }
  interface FinancePaymentItemProps {
    amount?: number;
  }

  type MasterStatusCode =
    | ""
    | "RG-01"
    | "D151-01"
    | "D151-02"
    | "D151-03"
    | "D151-04"
    | "US-01"
    | "GRC-01"
    | "GRC-02"
    | "GRC-03"
    | "GRC-04"
    | "GRC-05"
    | "MNC-01"
    | "MNC-02"
    | "MNC-03";

  type RoleCode =
    | ""
    | "manager"
    | "consult"
    | "manager_consult"
    | "ultrasound"
    | "finance"
    | "development"
    | "stat"
    | "executive"
    | "counter"
    | "sale"
    | "administrator"
    | "user"
    | "assistant_manager_consult";
}

interface DataResponseProps {
  code: number;
  message: string;
  output: OutputResponseProps;
  status: boolean;
}
interface OutputResponseProps {
  data: any;
  messageAlert: MessageAlertResponseProps;
}
interface MessageAlertResponseProps {
  en: string;
  th: string;
}

interface DataResponseErrorProps {
  data: OutputResponseErrorProps;
}
interface OutputResponseErrorProps {
  code?: number;
  message?: string;
  status?: boolean;
}

export {};
