import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import { LoginFromProps } from "../_types/Login";

type LoginState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  validationData: (
    t: (key: string) => string
  ) => yup.ObjectSchema<{}, LoginFromProps, {}, "">;
  clientLogin: (
    telephoneNumber?: string,
    dateOfBirth?: string,
    telephoneCode?: string
  ) => Promise<ResponseProps>;
};

export const useLoginState = create<LoginState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  validationData: (t) => {
    return yup.object<LoginFromProps>({
      telephoneNumber: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      dateOfBirth: yup.lazy((value) => {
        if (typeof value === "string") {
          return yup.string().required("validate.field");
        } else if (value instanceof Date) {
          return yup
            .date()
            .required("validate.field")
            .min(new Date(1900, 0, 1), "validate.field");
        } else {
          return yup.mixed().notRequired();
        }
      }),
    });
  },
  clientLogin: async (telephoneNumber, dateOfBirth, telephoneCode) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClient/login`;
    await axios
      .post(url, {
        telephoneNumber: telephoneNumber,
        dateOfBirth: dateOfBirth,
        telephoneCode: telephoneCode,
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
