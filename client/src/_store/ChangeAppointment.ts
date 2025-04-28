import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import {
  CancelAppointmentRequestApiProps,
  TransCaseApiProps,
  AppointmentRequestFormProps,
} from "../_types/ChangeAppointment";

type ChangeAppointmentState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  validationPostponeAppointment: (
    t: (key: string) => string
  ) => yup.ObjectSchema<{}, AppointmentRequestFormProps, {}, "">;
  getAppointment: (
    data?: CancelAppointmentRequestApiProps
  ) => Promise<ResponseProps>;
  getCaseById: (id: string) => Promise<ResponseProps>;
  requestCancelAppointment: (
    data?: CancelAppointmentRequestApiProps
  ) => Promise<ResponseProps>;
  requestUpdateAppointment: (
    data?: TransCaseApiProps
  ) => Promise<ResponseProps>;
};

export const useChangeAppointmentState = create<ChangeAppointmentState>(
  (set: any, get: any) => ({
    isLoading: false,
    setIsLoading: (isLoading) => {
      set({ isLoading: isLoading });
    },
    validationPostponeAppointment: (t) => {
      return yup.object<AppointmentRequestFormProps>({
        appointmentData: yup
          .date()
          .required("validate.field")
          .min(new Date(1900, 0, 1), "validate.field")
          .typeError("validate.field"),
      });
    },
    getAppointment: async (data) => {
      var resultForm: ResponseProps = {};

      const url = `${API_APP_URL}/api/TransCase`;
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
    getCaseById: async (id) => {
      var resultForm: ResponseProps = {};

      const url = `${API_APP_URL}/api/TransCase/${id}`;
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
    requestCancelAppointment: async (data) => {
      var resultForm: ResponseProps = {};

      const url = `${API_APP_URL}/api/TransCase/updateStatus`;
      await axios
        .post(url, {
          id: data?.id,
          masterStatusCode: data?.statusCode,
        })
        .then((res: ResponseProps) => {
          resultForm = res;
        })
        .catch((error: any) => {
          resultForm = error;
        });
      return resultForm;
    },
    requestUpdateAppointment: async (data) => {
      var resultForm: ResponseProps = {};

      const url = `${API_APP_URL}/api/TransCase/updateTransCase`;
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
  })
);
