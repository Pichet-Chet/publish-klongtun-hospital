import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import {
  CaseInfoTabContentFromProps,
  CaseInfoTabActionFromProps,
} from "../_types/Case";

type CaseState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingCaseInfoTab: boolean;
  setIsLoadingCaseInfoTab: (isLoading: boolean) => void;
  isLoadingConsultTab: boolean;
  setIsLoadingConsultTab: (isLoading: boolean) => void;
  isLoadingActionTab: boolean;
  setIsLoadingActionTab: (isLoading: boolean) => void;
  validationData: () => yup.ObjectSchema<
    {},
    CaseInfoTabContentFromProps,
    {},
    ""
  >;
  validationModalData: () => yup.ObjectSchema<{}, any, {}, "">;
  validationActionModalData: () => yup.ObjectSchema<{}, any, {}, "">;
  validationActionData: () => yup.ObjectSchema<
    {},
    CaseInfoTabActionFromProps,
    {},
    ""
  >;
  getTransCaseById: (uid?: string) => Promise<ResponseProps>;
  getTransConsultRoomByCaseId: (caseId?: string) => Promise<ResponseProps>;
  getTransConsultCommentByCaseId: (caseId?: string) => Promise<ResponseProps>;
  saveComment: (
    transCaseId?: string,
    createName?: string,
    comment?: string
  ) => Promise<ResponseProps>;
  saveTransConsultRoom: (
    data?: CaseInfoTabContentFromProps
  ) => Promise<ResponseProps>;
  saveTransCaseCancel: (
    transCaseId?: string,
    masterReasonNotTreatmentId?: string,
    remark?: string,
    createdBy?: string
  ) => Promise<ResponseProps>;
  saveTransCaseReOpen: (
    oldCaseId?: string,
    newAppointment?: string,
    remark?: string,
    isFreeUs?: boolean,
    createdBy?: string
  ) => Promise<ResponseProps>;
  updateStatusCase: (
    caseId?: string,
    statusCode?: MasterStatusCode
  ) => Promise<ResponseProps>;
  assignConsultRoom: (
    caseId?: string,
    remark?: string
  ) => Promise<ResponseProps>;
  updateStartConsult: (caseId?: string) => Promise<ResponseProps>;
};

export const useCaseState = create<CaseState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  isLoadingCaseInfoTab: false,
  setIsLoadingCaseInfoTab: (isLoading) => {
    set({ isLoadingCaseInfoTab: isLoading });
  },
  isLoadingConsultTab: false,
  setIsLoadingConsultTab: (isLoading) => {
    set({ isLoadingConsultTab: isLoading });
  },
  isLoadingActionTab: false,
  setIsLoadingActionTab: (isLoading) => {
    set({ isLoadingActionTab: isLoading });
  },
  validationData: () => {
    return yup.object<CaseInfoTabContentFromProps>({
      masterGestationalAgeId: yup
        .string()
        .nullable()
        .test(
          "masterGestationalAgeId",
          "กรุณาระบุข้อมูล",
          function (item, context) {
            var parent = _.last(context.from)
              ?.value as CaseInfoTabContentFromProps;
            if (parent.onAction === "update") {
              if (parent.masterGestationalAgeId === "") {
                return false;
              }
            }
            return true;
          }
        ),
      withdraw: yup
        .string()
        .nullable()
        .test("withdraw", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as CaseInfoTabContentFromProps;
          if (parent.onAction === "update") {
            if (parent.withdraw === "") {
              return false;
            }
          }
          return true;
        }),
      drugAllergyRemark: yup
        .string()
        .nullable()
        .test("drugAllergyRemark", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as CaseInfoTabContentFromProps;
          if (parent.onAction === "update") {
            if (
              parent.drugAllergy === true &&
              parent.drugAllergyRemark === ""
            ) {
              return false;
            }
          }
          return true;
        }),
      congenitalDiseaseRemark: yup
        .string()
        .nullable()
        .test(
          "congenitalDiseaseRemark",
          "กรุณาระบุข้อมูล",
          function (item, context) {
            var parent = _.last(context.from)
              ?.value as CaseInfoTabContentFromProps;
            if (parent.onAction === "update") {
              if (
                parent.congenitalDisease === true &&
                parent.congenitalDiseaseRemark === ""
              ) {
                return false;
              }
            }
            return true;
          }
        ),
      comment: yup
        .string()
        .nullable()
        .test("comment", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as CaseInfoTabContentFromProps;
          if (parent.onAction === "comment") {
            if (parent.comment === "") {
              return false;
            }
          }
          return true;
        }),
    });
  },
  validationModalData: () => {
    return yup.object<any>({
      referralFromName: yup
        .string()
        .nullable()
        .test("referralFromName", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)?.value as any;
          if (parent.referralFromName === "") {
            return false;
          }
          return true;
        }),
    });
  },
  validationActionModalData: () => {
    return yup.object<any>({
      reasonNotTreatmentName: yup
        .string()
        .nullable()
        .test(
          "reasonNotTreatmentName",
          "กรุณาระบุข้อมูล",
          function (item, context) {
            var parent = _.last(context.from)?.value as any;
            if (parent.reasonNotTreatmentName === "") {
              return false;
            }
            return true;
          }
        ),
    });
  },
  validationActionData: () => {
    return yup.object<CaseInfoTabActionFromProps>({
      cancelMasterReasonNotTreatmentId: yup
        .string()
        .nullable()
        .test(
          "cancelMasterReasonNotTreatmentId",
          "กรุณาระบุข้อมูล",
          function (item, context) {
            var parent = _.last(context.from)
              ?.value as CaseInfoTabActionFromProps;
            if (parent.onAction === "cancel") {
              if (parent.cancelMasterReasonNotTreatmentId === "") {
                return false;
              }
            }
            return true;
          }
        ),
      cancelRemark: yup
        .string()
        .nullable()
        .test("cancelRemark", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as CaseInfoTabActionFromProps;
          if (parent.onAction === "cancel") {
            if (parent.cancelRemark === "") {
              return false;
            }
          }
          return true;
        }),
      reopenNewAppointment: yup.lazy((value) => {
        if (typeof value === "string") {
          return yup
            .string()
            .nullable()
            .test(
              "reopenNewAppointment",
              "กรุณาระบุข้อมูล",
              function (item, context) {
                var parent = _.last(context.from)
                  ?.value as CaseInfoTabActionFromProps;
                if (parent.onAction === "reopen") {
                  if (!parent.reopenNewAppointment) {
                    return false;
                  }
                }
                return true;
              }
            );
        } else if (value instanceof Date) {
          return yup
            .date()
            .test(
              "reopenNewAppointment",
              "กรุณาระบุข้อมูล",
              function (item, context) {
                var parent = _.last(context.from)
                  ?.value as CaseInfoTabActionFromProps;
                if (parent.onAction === "reopen") {
                  if (!parent.reopenNewAppointment) {
                    return false;
                  }
                }
                return true;
              }
            );
        } else {
          return yup.mixed().notRequired();
        }
      }),
      reopenRemark: yup
        .string()
        .nullable()
        .test("reopenRemark", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as CaseInfoTabActionFromProps;
          if (parent.onAction === "reopen") {
            if (parent.reopenRemark === "") {
              return false;
            }
          }
          return true;
        }),
      reopenIsFreeUs: yup
        .string()
        .nullable()
        .test("reopenIsFreeUs", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as CaseInfoTabActionFromProps;
          if (parent.onAction === "reopen") {
            if (parent.reopenIsFreeUs === "") {
              return false;
            }
          }
          return true;
        }),
      sendToManagerRemark: yup
        .string()
        .nullable()
        .test(
          "sendToManagerRemark",
          "กรุณาระบุข้อมูล",
          function (item, context) {
            var parent = _.last(context.from)
              ?.value as CaseInfoTabActionFromProps;
            if (parent.onAction === "toManager") {
              if (parent.sendToManagerRemark === "") {
                return false;
              }
            }
            return true;
          }
        ),
    });
  },
  getTransCaseById: async (uid) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/${uid}`;
    await axios
      .get(url)
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  getTransConsultRoomByCaseId: async (caseId) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransConsultRoom/caseId/${caseId}`;
    await axios
      .get(url)
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  getTransConsultCommentByCaseId: async (caseId) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransConsultComment/caseId/${caseId}`;
    await axios
      .get(url)
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  saveComment: async (transCaseId, createName, comment) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransConsultComment`;
    await axios
      .post(url, {
        transCaseId: transCaseId,
        description: comment,
        isActive: true,
        createdBy: createName,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  saveTransConsultRoom: async (data) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransConsultRoom`;
    await axios
      .post(url, { ...data })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  saveTransCaseCancel: async (
    transCaseId,
    masterReasonNotTreatmentId,
    remark,
    createdBy
  ) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCaseCancel`;
    await axios
      .post(url, {
        transCaseId: transCaseId,
        masterReasonNotTreatmentId: masterReasonNotTreatmentId,
        remark: remark,
        createdBy: createdBy,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  saveTransCaseReOpen: async (
    oldCaseId,
    newAppointment,
    remark,
    isFreeUs,
    createdBy
  ) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/CreateNewAppintment`;
    await axios
      .post(url, {
        oldCaseId: oldCaseId,
        newAppointment: newAppointment,
        remark: remark,
        isFreeUs: isFreeUs,
        createdBy: createdBy,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  updateStatusCase: async (caseId, statusCode) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/updateStatus`;
    await axios
      .post(url, {
        id: caseId,
        masterStatusCode: statusCode,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  assignConsultRoom: async (caseId, remark) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/AssignConsultRoom`;
    await axios
      .post(url, {
        caseId: caseId,
        usRemark: remark,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  updateStartConsult: async (caseId) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/updateStartConsult`;
    await axios
      .post(url, {
        id: caseId,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
}));
