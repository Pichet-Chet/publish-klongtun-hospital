import { create } from "zustand";
import _ from "lodash";
import { checkPermission } from "../_types/CheckPermission";
import { checkStatus } from "../_types/CheckStatus";

type CheckState = {
  checkPermissionControl: (
    controlId: string,
    roles: RoleCode,
    type: "show" | "edit"
  ) => boolean;
  checkStatusControl: (
    controlId: string,
    caseStatus: MasterStatusCode,
    type: "show" | "edit"
  ) => boolean;
};

export const useCheckState = create<CheckState>((set: any, get: any) => ({
  checkPermissionControl: (controlId, roles, type) => {
    const profileHaveRole =
      checkPermission.find((x) => (x.role || []).includes(roles)) || {};
    const isEnabled: boolean =
      type === "edit"
        ? (profileHaveRole?.editControls || []).includes(controlId)
        : (profileHaveRole?.displayControls || []).includes(controlId);
    return isEnabled;
  },
  checkStatusControl: (controlId, caseStatus, type) => {
    const haveStatus =
      checkStatus.find((x) => (x.status || []).includes(caseStatus)) || {};
    const isEnabled: boolean =
      type === "edit"
        ? (haveStatus?.editControls || []).includes(controlId)
        : (haveStatus?.displayControls || []).includes(controlId);
    return caseStatus === "" ? false : isEnabled;
  },
}));
