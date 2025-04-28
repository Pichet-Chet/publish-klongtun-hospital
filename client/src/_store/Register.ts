import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import {
  RegisterUserFromProps,
  RegisterUserRequestFormProps,
  RegisterUserRequestApiProps,
} from "../_types/Register";

type RegisterState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  validationRequestOtpData: (
    t: (key: string) => string
  ) => yup.ObjectSchema<{}, RegisterUserFromProps, {}, "">;
  validationRequestRegisterData: (
    t: (key: string) => string
  ) => yup.ObjectSchema<{}, RegisterUserRequestFormProps, {}, "">;
  verifyMobilePhoneAndDateOfBirth: (
    phone?: string,
    dateOfBirth?: string
  ) => Promise<ResponseProps>;
  findWithPhone: (
    phone?: string,
    // dateOfBirth?: string
  ) => Promise<ResponseProps>;
  requestRegister: (
    data?: RegisterUserRequestApiProps
  ) => Promise<ResponseProps>;
  getNameByRefCode: (refCode?: string) => Promise<ResponseProps>;
};

export const useRegisterState = create<RegisterState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  validationRequestOtpData: (t) => {
    return yup.object<RegisterUserFromProps>({
      telephoneNumber: yup
        .string()
        .test("telephoneNumber", "validate.field", function (item, context) {
          return item === "" ? false : true;
        }),
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
  validationRequestRegisterData: (t) => {
    return yup.object<RegisterUserRequestFormProps>({
      tranSaleRefCode: yup.string().required("validate.field"),
      fullName: yup
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
      telephoneNumber: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      masterNationality: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      historyOfCesareanSection: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      drugAllergy: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      congenitalDisease: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      marriedOrBoyfriend: yup
        .string()
        .required("validate.field")
        .typeError("validate.field"),
      receiveServiceDate: yup.lazy((value) => {
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
      informationToDoctor: yup
        .string()
        .test(
          "informationToDoctor",
          t("validate.max256"),
          (val) => (val?.length || 0) <= 256
        ),
    });
  },
  verifyMobilePhoneAndDateOfBirth: async (phone, dateOfBirth) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClient/VerifyMobilePhoneAndDateOfBirth`;
    await axios
      .post(url, {
        phone: phone,
        dateOfBirth: dateOfBirth,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  findWithPhone: async (phone) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClient/findWithPhone`;
    await axios
      .post(url, {
        code: "+66",
        phone: phone,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  requestRegister: async (data) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransClient/register`;
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
  getNameByRefCode: async (refCode) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransSale/byRefCode/${refCode}`;
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
