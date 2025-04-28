import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import moment from "moment-timezone";

type CaseListState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  getCaseToday: (
    statusCode: MasterStatusCode[],
    textSearch?: string
  ) => Promise<ResponseProps>;
};

export const useCaseListState = create<CaseListState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  getCaseToday: async (statusCode, textSearch) => {
    var resultForm: ResponseProps = {};
    var dateToday = moment.tz("Asia/Bangkok").format("YYYY-MM-DD");
    var search = textSearch ? `&TextSearch=${textSearch}` : "";
    var status = "";
    if (statusCode.length > 0) {
      statusCode.map((a) => {
        status += `&MasterStatusCodes=${a}`;
      });
    }
    const url = `${API_APP_URL}/api/TransCase?IsAll=true&StartReceiveServiceDate=${dateToday}&EndReceiveServiceDate=${dateToday}${search}${status}`;
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
