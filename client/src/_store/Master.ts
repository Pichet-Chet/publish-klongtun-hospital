import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import { MasterCountryProps, MasterNationalityProps } from "../_types/Master";

type MasterState = {
  isLoading: boolean;
  masterNationality: MasterNationalityProps[];
  setIsLoading: (isLoading: boolean) => void;
  setMasterNationality: (data: MasterNationalityProps[]) => void;
  getMasterNationality: () => Promise<ResponseProps>;
};

export const useMasterState = create<MasterState>((set: any, get: any) => ({
  isLoading: false,
  masterNationality: [],
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  setMasterNationality: (data) => {
    set({ masterNationality: data });
  },
  getMasterNationality: async () => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterNationality?IsAll=true`;
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
