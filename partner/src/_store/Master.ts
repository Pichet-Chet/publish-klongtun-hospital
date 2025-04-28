import { create } from "zustand";
import axios, { API_APP_URL } from "../_lib/Axios";
import * as yup from "yup";
import _ from "lodash";
import {
  AllMasterProps,
  MasterCountryProps,
  MasterNationalityProps,
  MasterRightTreatmentProps,
  MasterGestationalAgeProps,
  MasterReferralFromProps,
  MasterReasonNotTreatmentProps,
} from "../_types/Master";

type MasterState = {
  isLoading: boolean;
  allMaster: AllMasterProps[];
  masterCountry: MasterCountryProps[];
  masterNationality: MasterNationalityProps[];
  masterRightTreatment: MasterRightTreatmentProps[];
  masterGestationalAge: MasterGestationalAgeProps[];
  masterReferralFrom: MasterReferralFromProps[];
  masterReasonNotTreatment: MasterReasonNotTreatmentProps[];
  setIsLoading: (isLoading: boolean) => void;
  setAllMaster: (data: AllMasterProps[]) => void;
  setMasterCountry: (data: MasterCountryProps[]) => void;
  setMasterNationality: (data: MasterNationalityProps[]) => void;
  setMasterRightTreatment: (data: MasterRightTreatmentProps[]) => void;
  setMasterGestationalAge: (data: MasterGestationalAgeProps[]) => void;
  setMasterReferralFrom: (data: MasterReferralFromProps[]) => void;
  setMasterReasonNotTreatment: (data: MasterReasonNotTreatmentProps[]) => void;
  getAllMaster: (data: string[]) => Promise<ResponseProps>;
  getMasterCountry: () => Promise<ResponseProps>;
  getMasterNationality: () => Promise<ResponseProps>;
  getMasterRightTreatment: () => Promise<ResponseProps>;
  getMasterGestationalAge: () => Promise<ResponseProps>;
  getMasterReferralFrom: () => Promise<ResponseProps>;
  createMasterRightTreatment: (
    name?: string,
    createBy?: string
  ) => Promise<ResponseProps>;
  createMasterReferralFrom: (
    name?: string,
    createBy?: string
  ) => Promise<ResponseProps>;
  createMasterReasonNotTreatment: (
    name?: string,
    createBy?: string
  ) => Promise<ResponseProps>;
};

export const useMasterState = create<MasterState>((set: any, get: any) => ({
  isLoading: false,
  allMaster: [],
  masterCountry: [],
  masterNationality: [],
  masterRightTreatment: [],
  masterGestationalAge: [],
  masterReferralFrom: [],
  masterReasonNotTreatment: [],
  setIsLoading: (isLoading) => {
    set({ isLoading: isLoading });
  },
  setAllMaster: (data) => {
    set({ allMaster: data });
  },
  setMasterCountry: (data) => {
    set({ masterCountry: data });
  },
  setMasterNationality: (data) => {
    set({ masterNationality: data });
  },
  setMasterRightTreatment: (data) => {
    set({ masterRightTreatment: data });
  },
  setMasterGestationalAge: (data) => {
    set({ masterGestationalAge: data });
  },
  setMasterReferralFrom: (data) => {
    set({ masterReferralFrom: data });
  },
  setMasterReasonNotTreatment: (data) => {
    set({ masterReasonNotTreatment: data });
  },
  getAllMaster: async (data) => {
    var resultForm: ResponseProps = {};
    var params = "";
    if (data.length > 0) {
      data.map((a, index) => {
        params += `${index !== 0 ? "&" : ""}param=${a}`;
        return params;
      });
    }
    const url = `${API_APP_URL}/api/MasterCenter?${params}`;
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
  getMasterCountry: async () => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterCountry?IsAll=true`;
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
  getMasterRightTreatment: async () => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterRightTreatment?IsAll=true`;
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
  getMasterGestationalAge: async () => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterGestationalAge?IsAll=true`;
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
  getMasterReferralFrom: async () => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterReferralFrom?IsAll=true`;
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
  createMasterRightTreatment: async (name, createBy) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterRightTreatment`;
    await axios
      .post(url, {
        name: name,
        isActive: true,
        createdBy: createBy,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  createMasterReferralFrom: async (name, createBy) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterReferralFrom`;
    await axios
      .post(url, {
        name: name,
        isActive: true,
        createdBy: createBy,
      })
      .then((res: ResponseProps) => {
        resultForm = res;
      })
      .catch((error: any) => {
        resultForm = error;
      });
    return resultForm;
  },
  createMasterReasonNotTreatment: async (name, createBy) => {
    var resultForm: ResponseProps = {};
    const url = `${API_APP_URL}/api/MasterReasonNotTreatment`;
    await axios
      .post(url, {
        name: name,
        isActive: true,
        createdBy: createBy,
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
