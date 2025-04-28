import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import moment from "moment-timezone";
import { UnfollowRequest, UpdateRemarkSaleRequest, UpdateReceiveServiceDateRequest, CaseDetail, FindWithPhoneRequest, CreateTransClientRequest } from "@/_types/Home";
import { statSync } from "fs";

type HomeState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  getHomePage: (saleId: string) => Promise<ResponseProps>;
  getCaseFilter: (
    saleId: string,
    dateSelect?: [Date | null, Date | null] | undefined,
    textSearch?: string
  ) => Promise<ResponseProps>;
  getCountCase: (saleId: string) => Promise<ResponseProps>;
  getMasterReasonUnfollow: () => Promise<ResponseProps>;
  unfollow: (data?: UnfollowRequest) => Promise<ResponseProps>;
  updateRemarkSale: (data?: UpdateRemarkSaleRequest) => Promise<ResponseProps>;
  updateReceiveServiceDate: (data?: UpdateReceiveServiceDateRequest) => Promise<ResponseProps>;
  caseDetail?: CaseDetail | null;
  verifyTelephone: (data?: FindWithPhoneRequest) => Promise<ResponseProps>;
  verifyTelephoneWithCaseRegister: (data?: FindWithPhoneRequest) => Promise<ResponseProps>;
  getTransSaleGroup: (groupId?: string) => Promise<ResponseProps>;
  createTransClient: (data?: CreateTransClientRequest) => Promise<ResponseProps>;
};

export const useHomeState = create<HomeState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  getHomePage: async (saleId) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/caseForSale?SaleId=${saleId}&SortName=updateDate&SortType=desc`;
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
  getCaseFilter: async (saleId, dateSelect, textSearch) => {
    var resultForm: ResponseProps = {};
    var sDate = '';
    var eDate = '';
    if (dateSelect !== undefined) {
      sDate = `&StartDate=${moment.tz(dateSelect[0], "Asia/Bangkok").format("YYYY-MM-DD")}`
      eDate = `&EndDate=${moment.tz(dateSelect[1], "Asia/Bangkok").format("YYYY-MM-DD")}`
    }

    var search = textSearch ? `&TextSearch=${textSearch}` : "";
    const url = `${API_APP_URL}/api/TransCase/caseForSale?SaleId=${saleId}${sDate}${eDate}${search}`;
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
  getCountCase: async (saleId) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransCase/countCaseForSale/${saleId}`;
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
  getMasterReasonUnfollow: async () => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterReasonUnFollow?IsActive=true&IsAll=true`;
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
  unfollow: async (data?: UnfollowRequest) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransCase/unfollowCase`;
    await axios
      .patch(url, { ...data })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  updateRemarkSale: async (data?: UpdateRemarkSaleRequest) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransCase/updateRemarkSale`;
    await axios
      .patch(url, { ...data })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  updateReceiveServiceDate: async (data?: UpdateReceiveServiceDateRequest) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransCase/updateReceiveServiceDate`;
    await axios
      .patch(url, { ...data })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  verifyTelephone: async (data?: FindWithPhoneRequest) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransClient/findWithPhone`;
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
  verifyTelephoneWithCaseRegister: async (data?: FindWithPhoneRequest) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransClient/GetTransClientWithPhoneAndRegisterCase`;
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
  getTransSaleGroup: async (groupId?: string) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransSale/byGroup/${groupId}?IsAll=true`;
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
  createTransClient: async (data?: CreateTransClientRequest) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/TransClient/partner/createTransClient`;
    await axios
      .post(url, { ...data })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  }
}));
