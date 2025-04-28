export const checkStatus: CheckStatusControl[] = [
  {
    status: ["RG-01"],
    displayControls: ["caseList.receiveUs", "home.receiveUs"],
    editControls: [],
  },
  {
    status: ["D151-03"],
    displayControls: ["waitingUs.acceptUs"],
    editControls: [],
  },
  {
    status: ["D151-04"],
    displayControls: ["caseList.waitingUs", "home.waitingUs"],
    editControls: [],
  },
  {
    status: ["US-01"],
    displayControls: ["caseList.startConsult", "home.startConsult"],
    editControls: [],
  },
  {
    status: ["GRC-01"],
    displayControls: ["actionTab.sendToManager"],
    editControls: [],
  },
  {
    status: ["GRC-02"],
    displayControls: ["home.startManagerConsult"],
    editControls: [],
  },
  {
    status: ["MNC-02", "MNC-03"],
    displayControls: [
      "caseDetail.actionTabContent",
      "clientDetail.clientInfoTab.addMasterRightTreatment",
      "clientDetail.clientInfoTab.onUpdateData",
      "clientDetail.clientInfoTab.onSubmitComment",
      "caseDetail.consultTab.onOpenPopup",
      "caseDetail.consultTab.onUpdateData",
      "caseDetail.consultTab.onSubmitComment",
    ],
    editControls: [
      "clientDetail.clientInfoTab.fullName",
      "clientDetail.clientInfoTab.citizenIdentification",
      "clientDetail.clientInfoTab.passportNumber",
      "clientDetail.clientInfoTab.masterNationalityId",
      "clientDetail.clientInfoTab.dateOfBirth",
      "clientDetail.clientInfoTab.telephoneNumber",
      "clientDetail.clientInfoTab.telephoneSecond",
      "clientDetail.clientInfoTab.occupation",
      "clientDetail.clientInfoTab.address",
      "clientDetail.clientInfoTab.masterRightTreatmentId",
      "clientDetail.clientInfoTab.hostpitalName",
      "clientDetail.clientInfoTab.hnNo",
      "clientDetail.clientInfoTab.comment",
      "caseDetail.consultTab.masterGestationalAgeId",
      "caseDetail.consultTab.masterReferralFromId",
      "caseDetail.consultTab.withdraw",
      "caseDetail.consultTab.drugAllergy",
      "caseDetail.consultTab.drugAllergyRemark",
      "caseDetail.consultTab.congenitalDisease",
      "caseDetail.consultTab.congenitalDiseaseRemark",
      "caseDetail.consultTab.caesareanSection",
      "caseDetail.consultTab.relatives",
      "caseDetail.consultTab.patient",
      "caseDetail.consultTab.comment",
    ],
  },
];

export type CheckStatusControl = {
  status?: MasterStatusCode[];
  displayControls?: string[];
  editControls?: string[];
};
