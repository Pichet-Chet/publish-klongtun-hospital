import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import moment from "moment-timezone";

type ClientListState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  getClient: (textSearch?: string) => Promise<ResponseProps>;
};

export const useClientListState = create<ClientListState>(
  (set: any, get: any) => ({
    isLoading: false,
    setIsLoading: (isLoading) => {
      set({ isLoading: isLoading });
    },
    getClient: async (textSearch) => {
      var resultForm: ResponseProps = {};
      var search = textSearch ? `&TextSearch=${textSearch}` : "";
      const url = `${API_APP_URL}/api/TransClient?IsAll=true${search}`;
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
  })
);
