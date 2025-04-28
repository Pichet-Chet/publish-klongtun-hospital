import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import { ClientInfoTabContentFromProps } from "../_types/Client";

type ClientState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  isLoadingClientInfoTab: boolean;
  setIsLoadingClientInfoTab: (isLoading: boolean) => void;
  isLoadingCaseInfoTab: boolean;
  setIsLoadingCaseInfoTab: (isLoading: boolean) => void;
  isLoadingPaymentTab: boolean;
  setIsLoadingPaymentTab: (isLoading: boolean) => void;
  validationData: () => yup.ObjectSchema<
    {},
    ClientInfoTabContentFromProps,
    {},
    ""
  >;
  validationModalData: () => yup.ObjectSchema<{}, any, {}, "">;
  getProfile: (uid?: string) => Promise<ResponseProps>;
  getPermission: (roleId?: string) => Promise<ResponseProps>;
  getTransClientById: (uid?: string) => Promise<ResponseProps>;
  getTransClientCommentByClientId: (uid?: string) => Promise<ResponseProps>;
  saveComment: (
    uuid?: string,
    createName?: string,
    comment?: string
  ) => Promise<ResponseProps>;
  saveClientDetail: (data?: TransClientProps) => Promise<ResponseProps>;
  getCaseByClientId: (clientId?: string) => Promise<ResponseProps>;
  getPaymentByClientId: (clientId?: string) => Promise<ResponseProps>;
  getPaymentByCaseId: (caseId?: string) => Promise<ResponseProps>;
};

export const useClientState = create<ClientState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  isLoadingClientInfoTab: false,
  setIsLoadingClientInfoTab: (isLoading) => {
    set({ isLoadingClientInfoTab: isLoading });
  },
  isLoadingCaseInfoTab: false,
  setIsLoadingCaseInfoTab: (isLoading) => {
    set({ isLoadingCaseInfoTab: isLoading });
  },
  isLoadingPaymentTab: false,
  setIsLoadingPaymentTab: (isLoading) => {
    set({ isLoadingPaymentTab: isLoading });
  },
  validationData: () => {
    return yup.object<ClientInfoTabContentFromProps>({
      clientData: yup.object<TransClientProps>({
        fullName: yup
          .string()
          .nullable()
          .test(
            "clientData.fullName",
            "กรุณาระบุข้อมูล",
            function (item, context) {
              var parent = _.last(context.from)
                ?.value as ClientInfoTabContentFromProps;
              if (parent.onAction === "update") {
                if (parent.clientData?.fullName === "") {
                  return false;
                }
              }
              return true;
            }
          ),
        masterNationalityId: yup
          .string()
          .nullable()
          .test(
            "clientData.masterNationalityId",
            "กรุณาระบุข้อมูล",
            function (item, context) {
              var parent = _.last(context.from)
                ?.value as ClientInfoTabContentFromProps;
              if (parent.onAction === "update") {
                if (parent.clientData?.masterNationalityId === "") {
                  return false;
                }
              }
              return true;
            }
          ),
        telephoneNumber: yup
          .string()
          .nullable()
          .test(
            "clientData.telephoneNumber",
            "กรุณาระบุข้อมูล",
            function (item, context) {
              var parent = _.last(context.from)
                ?.value as ClientInfoTabContentFromProps;
              if (parent.onAction === "update") {
                if (parent.clientData?.telephoneNumber === "") {
                  return false;
                }
              }
              return true;
            }
          ),
        masterRightTreatmentId: yup
          .string()
          .nullable()
          .test(
            "clientData.masterRightTreatmentId",
            "กรุณาระบุข้อมูล",
            function (item, context) {
              const parent = _.last(context.from)
                ?.value as ClientInfoTabContentFromProps;
              if (parent.onAction === "update") {
                if (item === "" || item === null) {
                  return false;
                }
              }
              return true;
            }
          ),
      }),
      comment: yup
        .string()
        .nullable()
        .test("comment", "กรุณาระบุข้อมูล", function (item, context) {
          var parent = _.last(context.from)
            ?.value as ClientInfoTabContentFromProps;
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
      rightTreatmentName: yup
        .string()
        .nullable()
        .test(
          "rightTreatmentName",
          "กรุณาระบุข้อมูล",
          function (item, context) {
            var parent = _.last(context.from)?.value as any;
            if (parent.rightTreatmentName === "") {
              return false;
            }
            return true;
          }
        ),
    });
  },
  getProfile: async (uid) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransSale/${uid}`;
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
  getPermission: async (roleId) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/SysPermission/byRole/${roleId}`;
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
  getTransClientById: async (uid) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClient/${uid}`;
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
  getTransClientCommentByClientId: async (uid) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClientComment?TransClientId=${uid}`;
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
  saveComment: async (uuid, createName, comment) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClientComment`;
    await axios
      .post(url, {
        transClientId: uuid,
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
  saveClientDetail: async (data) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClient/updateTransClient`;
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
  getCaseByClientId: async (clientId) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransCase?IsAll=true&TransClientId=${clientId}&SortName=receiveServiceDate&SortType=desc`;
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
  getPaymentByClientId: async (clientId) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/Finance/transPayment?IsAll=true&TransClientId=${clientId}`;
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
  getPaymentByCaseId: async (caseId) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/Finance/transPayment?IsAll=true&TransCaseId=${caseId}`;
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
}));
