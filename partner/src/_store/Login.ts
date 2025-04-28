import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import { LoginFromProps } from "../_types/Login";

type LoginState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  validationData: () => yup.ObjectSchema<{}, LoginFromProps, {}, "">;
  clientLogin: (username?: string, password?: string) => Promise<ResponseProps>;
};

export const useLoginState = create<LoginState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  validationData: () => {
    return yup.object<LoginFromProps>({
      userName: yup.string().required("กรุณาระบุข้อมูล"),
      password: yup.string().required("กรุณาระบุข้อมูล"),
    });
  },
  clientLogin: async (username, password) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransSale/login`;
    await axios
      .post(url, {
        username: username,
        password: password,
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
