import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";

type ClientState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  getProfile: (uid?: string) => Promise<ResponseProps>;
  getCaesRg01: (uid?: string) => Promise<ResponseProps>;
};

export const useClientState = create<ClientState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  getProfile: async (uid) => {
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
  getCaesRg01: async (uid) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase/validateRG01/${uid}`;
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
