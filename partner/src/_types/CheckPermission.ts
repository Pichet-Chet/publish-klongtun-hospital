export const checkPermission: CheckPermissionControl[] = [
  {
    role: ["administrator"],
    displayControls: [
      "home.receiveUs",
      "caseDetail.caseInfoTab.printDetail",
      "actionTab.sendToManager",
    ],
    editControls: [],
  },
  {
    role: ["manager"],
    displayControls: ["home.receiveUs"],
    editControls: [],
  },
  {
    role: ["counter"],
    displayControls: ["home.receiveUs", "caseDetail.caseInfoTab.printDetail"],
    editControls: [],
  },
  {
    role: ["consult"],
    displayControls: ["actionTab.sendToManager"],
    editControls: [],
  },
  {
    role: ["manager_consult"],
    displayControls: ["actionTab.actionForManager"],
    editControls: [],
  },
];

export type CheckPermissionControl = {
  role?: RoleCode[];
  displayControls?: string[];
  editControls?: string[];
};
