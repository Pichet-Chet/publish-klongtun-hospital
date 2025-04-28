import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import {
  BookingUserRequestFormProps,
  BookingUserRequestApiProps,
} from "../_types/Booking";
import { TransClientProps } from "../_types/Client";

type BookingState = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
  validationRequestBookingData: (
    t: (key: string) => string
  ) => yup.ObjectSchema<{}, BookingUserRequestFormProps, {}, "">;
  updateClient: (data?: TransClientProps) => Promise<ResponseProps>;
  requestBooking: (data?: BookingUserRequestApiProps) => Promise<ResponseProps>;
};

export const useBookingState = create<BookingState>((set: any, get: any) => ({
  isLoading: false,
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  validationRequestBookingData: (t) => {
    return yup.object<BookingUserRequestFormProps>({
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
      gestationalAge: yup.lazy((value) => {
        if (typeof value === "string") {
          return yup.string().optional();
        } else if (value === "number") {
          return yup.number().positive("validate.field").optional();
        } else {
          return yup.mixed().notRequired();
        }
      }),
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
      receiveServiceDate: yup
        .date()
        .required("validate.field")
        .min(new Date(1900, 0, 1), "validate.field")
        .typeError("validate.field"),
      informationToDoctor: yup
        .string()
        .test(
          "informationToDoctor",
          "validate.max256",
          (val) => (val?.length || 0) <= 256
        ),
    });
  },
  updateClient: async (data) => {
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
  requestBooking: async (data) => {
    var resultForm: ResponseProps = {};

    const url = `${API_APP_URL}/api/TransCase`;
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
}));
