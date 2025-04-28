import React, { useEffect, useState, useRef } from "react";
import { Link } from "react-router-dom";
import { FormProvider, useForm, SubmitHandler } from "react-hook-form";
import BootstrapTable, {
  ColumnDescription,
  PaginationOptions,
} from "fad-react-bootstrap-table-next";
import paginationFactory from "react-bootstrap-table2-paginator";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useHomeState } from "../../_store/Home";
import {
  HomeFromProps,
  CountCase,
  CaseDetail,
  CaseForSale,
  MasterReasonUnFollow,
  UnfollowResponse,
  UpdateRemarkSaleResponse,
  UpdateReceiveServiceDateResponse,
} from "../../_types/Home";
import { useCaseState } from "../../_store/Case";
import { useCheckState } from "../../_store/Check";
import Spinner from "../../components/spinners/Spinner";
import { useAuth } from "../../context/UseAuth";
import { useHelper } from "../../context/Helper";
import InputFieldComponent, {
  InputMode,
} from "../../components/common/InputField";
import { toast } from "react-toastify";
import Chart from "react-apexcharts";
import JsonViewerComponent from "../../components/common/JsonViewerComponent";
import { set } from "lodash";
import DatePicker from "react-datepicker";
import moment from "moment-timezone";
import { FaCalendarAlt } from "react-icons/fa";
import Select from "react-select";
import { BsCopy, BsBuildingAdd } from "react-icons/bs";
import { FaEllipsisV } from "react-icons/fa";
import ModalAddClient from "../../components/home/ModalAddClient";

import {
  Input,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Label,
} from "reactstrap";

const MySwal = withReactContent(Swal);
let chartOption: any = {
  series: [
    {
      name: "ยอดการติดต่อ",
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    },
    {
      name: "ยอดเข้ารับการรักษา",
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    },
  ],
  chart: {
    height: 350,
    type: "line",
    dropShadow: {
      enabled: true,
      color: "#000",
      top: 18,
      left: 7,
      blur: 10,
      opacity: 0.2,
    },
    zoom: {
      enabled: false,
    },
    toolbar: {
      show: false,
    },
  },
  colors: ["#77B6EA", "#545454"],
  dataLabels: {
    enabled: true,
  },
  stroke: {
    curve: "smooth",
  },
  title: {
    text: "",
    align: "left",
  },
  grid: {
    borderColor: "#e7e7e7",
    row: {
      colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
      opacity: 0.5,
    },
  },
  markers: {
    size: 1,
  },
  xaxis: {
    categories: [
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "Jun",
      "Jul",
      "Aug",
      "Sep",
      "Oct",
      "Nov",
      "Dec",
    ],
    title: {
      text: "เดือน",
    },
  },
  legend: {
    position: "top",
    horizontalAlign: "right",
    floating: true,
    offsetY: -25,
    offsetX: -5,
  },
};

const Home = (props: any) => {
  const [chart, setChart] = useState(chartOption);
  const { getMessage } = useHelper();
  const { profile } = useAuth();
  const isLoading = useHomeState((state) => state.isLoading);
  const setIsLoading = useHomeState((state) => state.setIsLoading);
  const getHomePageDetail = useHomeState((state) => state.getHomePage);
  const getCountCase = useHomeState((state) => state.getCountCase);
  const getCaseFilter = useHomeState((state) => state.getCaseFilter);
  const [isOpenPopupAddClient, setIsOpenPopupAddClient] = useState(false);
  const getMasterReasonUnfollow = useHomeState(
    (state) => state.getMasterReasonUnfollow
  );
  const unfollow = useHomeState((state) => state.unfollow);
  const updateReceiveServiceDate = useHomeState(
    (state) => state.updateReceiveServiceDate
  );
  const updateRemarkSale = useHomeState((state) => state.updateRemarkSale);
  const [urlSale, setUrlSale] = useState("");

  const [isOpenPopup, setIsOpenPopup] = useState(false);
  const [modalType, setModalType] = useState("");
  const [ddlMasterReason, setDdlMasterReason] = useState<
    OptionMultiLanguageProps[]
  >([]);

  const methodsModal = useForm<any>({
    mode: "onChange",
    defaultValues: {
      caseId: "",
      remarkUs: "",
    },
  });
  const {
    resetField: resetFieldModal,
    watch: watchModal,
    trigger: triggerModal,
    setValue: setValueModal,
  } = methodsModal;

  const methods = useForm<HomeFromProps>({
    mode: "onChange",
    defaultValues: undefined,
  });
  const {
    formState: { errors },
    setValue,
    watch,
  } = methods;

  const columns: ColumnDescription<any, any>[] = [
    {
      dataField: "updateDate",
      text: "วันที่ทำการจองคิว",
      formatter: (cell: any, row: CaseDetail) => {
        return cell;
      },
      align: "center",
    },
    {
      dataField: "transCaseNo",
      text: "Case No",
      formatter: (cell: any, row: CaseDetail) => {
        return (
          <Label
            className="d-flex justify-content-center text-primary"
            style={{ cursor: "pointer", textDecoration: "underline" }}
            onClick={() => showPopup("showDetail", row.id, row)}
          >
            {cell}
          </Label>
        );
      },
    },
    {
      dataField: "fullName",
      text: "ชื่อ - นามสกุล",
      // headerClasses: "text-body-secondary",
      formatter: (cell: any, row: CaseDetail) => {
        // Check if cell has more than 4 characters
        const displayText = cell.length > 4 ? `${cell.slice(0, -4)}xxxx` : cell;

        return (
          <>
            <div className="d-flex justify-content-start form-label">
              {displayText}
              {row.isRsa ? (
                <a
                  href="#"
                  className="btn btn-success btn-sm disabled px-2 ms-2"
                  role="button"
                  aria-disabled="true"
                >
                  RSA
                </a>
              ) : null}
            </div>
          </>
        );
      }

    },

    {
      dataField: "receiveServiceDate",
      text: "วันที่นัดหมาย",
      align: "center",
    },
    {
      dataField: "saleFullName",
      text: "พนักงานขาย",
      align: "center",
    },
    {
      dataField: "statusName",
      align: "center",
      text: "สถานะ",
      formatter: (cell: any, row: CaseDetail) => {
        return (

          <Label
            className={`d-flex justify-content-center badge ${cell === "รอวันนัดหมาย" ? "bg-warning" : "bg-danger"} text-${cell === "รอวันนัดหมาย" ? "black" : "white"}`}
            style={{ cursor: "pointer", fontSize: "14px", fontWeight: "200" }}
          >
            {cell}
          </Label>

        );
      },
    },
    {
      dataField: "id",
      text: "Action",
      headerClasses: "text-body-secondary",
      formatter: (cell: any, row: CaseDetail) => {
        return (

          <div className="dropdown">
            <button
              className="btn btn-primary btn-sm"
              type="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
              style={{
                backgroundColor: 'transparent',
                color: 'black',
                border: 'none',
              }}
            >
              <FaEllipsisV />

            </button>
            <ul className="dropdown-menu dropdown-menu-dark">
              <li>
                <a
                  type="button"
                  className="dropdown-item"
                  href={`tel:${row.clientTel}`}
                >
                  โทรติดต่อ
                </a>
              </li>

              {profile().id === row.transSaleId ? (
                <React.Fragment>
                  <li>
                    <a
                      type="button"
                      className="dropdown-item"
                      onClick={() => showPopup("unfollow", cell, row)}
                    >
                      ยกเลิกการติดตาม
                    </a>
                  </li>
                  <li>
                    <a
                      type="button"
                      className="dropdown-item"
                      onClick={() => showPopup("addSaleComment", cell, row)}
                    >
                      เพิ่มบันทึกทีมขาย
                    </a>
                  </li>
                  <li>
                    {row.isRsa === true ? (
                      <a
                        type="button"
                        className="dropdown-item"
                        onClick={() => showPopup("addData", cell, row)}
                      >
                        เพิ่มข้อมูล/ระบุวันนัดหมาย
                      </a>
                    ) : null}
                  </li>
                </React.Fragment>
              ) : (<></>)}

            </ul>
          </div>



        );
      },
      align: "center",
    },
  ];
  const paginationOptions: PaginationOptions = {
    sizePerPage: 15,
    hideSizePerPage: true,
    hidePageListOnlyOnePage: true,
  };

  const copyToClipboard = () => {
    navigator.clipboard.writeText(urlSale).then(
      () => {
        toast.success("Copied Success");
      },
      (err) => {
        console.log(err);
      }
    );
  };

  const onClosePopup = () => {
    resetFieldModal("remarkUs");
    resetFieldModal("unfollowRequest");
    resetFieldModal("updateSaleRemarkRequest");
    resetFieldModal("masterReasonUnfollow");
    resetFieldModal("updateReceiveServiceDateRequest");
    setIsOpenPopup(false);
  };

  const onClosePopupAddClient = () => {
    setIsOpenPopupAddClient(false);
  };

  const onSaveModal: SubmitHandler<any> = async (data) => {
    setIsLoading(true);
    if (modalType === "unfollow") {
      data.unfollowRequest.caseId = data.caseId;
      await unfollow(data.unfollowRequest).then((res: ResponseProps) => {
        if (res.data?.code === 200) {
          if (res.data?.status === true) {
            var response = res.data.output.data as UnfollowResponse;
            MySwal.fire({
              icon: "success",
              text: `บันทึกสำเร็จ`,
              confirmButtonText: "ตกลง",
              confirmButtonColor: "#3085d6",
              allowOutsideClick: false,
              allowEscapeKey: false,
            }).then(async (result) => {
              onClosePopup();
              onLoadData().then(() => {
                setIsLoading(false);
              });
            });
            setIsLoading(false);
          } else {
            setIsLoading(false);
            toast.error(getMessage(res));
          }
        } else {
          setIsLoading(false);
          toast.error(getMessage(res));
        }
      });
    } else if (modalType === "addSaleComment") {
      data.updateRemarkSale.caseId = data.caseId;
      await updateRemarkSale(data.updateRemarkSale).then(
        (res: ResponseProps) => {
          if (res.data?.code === 200) {
            if (res.data?.status === true) {
              var response = res.data.output.data as UpdateRemarkSaleResponse;
              MySwal.fire({
                icon: "success",
                text: `บันทึกสำเร็จ`,
                confirmButtonText: "ตกลง",
                confirmButtonColor: "#3085d6",
                allowOutsideClick: false,
                allowEscapeKey: false,
              }).then(async (result) => {
                onClosePopup();
                onLoadData().then(() => {
                  setIsLoading(false);
                });
              });
              setIsLoading(false);
            } else {
              setIsLoading(false);
              toast.error(getMessage(res));
            }
          } else {
            setIsLoading(false);
            toast.error(getMessage(res));
          }
        }
      );
    } else if (modalType === "addData") {
      data.updateReceiveServiceDate.caseId = data.caseId;
      data.updateReceiveServiceDate.receiveServiceDate = moment
        .tz(data.updateReceiveServiceDate.receiveServiceDate, "Asia/Bangkok")
        .format("YYYY-MM-DD");
      await updateReceiveServiceDate(data.updateReceiveServiceDate).then(
        (res: ResponseProps) => {
          if (res.data?.code === 200) {
            if (res.data?.status === true) {
              var response = res.data.output
                .data as UpdateReceiveServiceDateResponse;
              MySwal.fire({
                icon: "success",
                text: `บันทึกสำเร็จ`,
                confirmButtonText: "ตกลง",
                confirmButtonColor: "#3085d6",
                allowOutsideClick: false,
                allowEscapeKey: false,
              }).then(async (result) => {
                onClosePopup();
                onLoadData().then(() => {
                  setIsLoading(false);
                });
              });
              setIsLoading(false);
            } else {
              setIsLoading(false);
              toast.error(getMessage(res));
            }
          } else {
            setIsLoading(false);
            toast.error(getMessage(res));
          }
        }
      );
    }
  };

  const onSearch = async () => {
    setIsLoading(true);
    await getCaseFilter(
      profile().id ?? "",
      watch().selectCreateDate,
      watch().textSearch
    ).then(async (caseResponse) => {
      if (caseResponse.data?.code === 200) {
        if (caseResponse.data?.status !== true) {
          toast.error(getMessage(caseResponse));
          await setValue("caseForSale", undefined);
        }
        var casesData = caseResponse.data?.output.data as CaseForSale;
        await setValue("caseForSale", casesData);
      } else {
        // toast.error(getMessage(caseResponse));
        await setValue("caseForSale", undefined);
      }
      setIsLoading(false);
    });
  };

  const onReset = async () => {
    setIsLoading(true);
    await onLoadData();
    await setValue("textSearch", "");
    await setValue("selectCreateDate", [null, null]);
    setIsLoading(false);
  };

  const onLoadData = async () => {
    await getHomePageDetail(profile().id ?? "").then(
      async (getHomePageDetail) => {
        if (getHomePageDetail.data?.code === 200) {
          if (getHomePageDetail.data?.status === true) {
            var data = getHomePageDetail.data?.output.data as CaseForSale;
            await setValue("caseForSale", data);
            setUrlSale(data.urlSale);
          } else {
            toast.error(getMessage(getHomePageDetail));
          }
        } else {
          toast.error(getMessage(getHomePageDetail));
        }
      }
    );
  };

  const loadCountCase = async () => {
    await getCountCase(profile().id ?? "").then(async (getCountCase) => {
      if (getCountCase.data?.code === 200) {
        if (getCountCase.data?.status === true) {
          var data = getCountCase.data?.output.data as CountCase;
          await setValue("countCase", data);
        } else {
          toast.error(getMessage(getCountCase));
        }
      } else {
        toast.error(getMessage(getCountCase));
      }
    });
  };

  const showPopup = async (
    btnName: string,
    caseId: string,
    data: CaseDetail
  ) => {
    setIsOpenPopup(true);
    if (btnName === "unfollow") {
      setModalType("unfollow");
      await getMasterReasonUnfollow().then(async (getMasterReasonUnfollow) => {
        if (getMasterReasonUnfollow.data?.code === 200) {
          if (getMasterReasonUnfollow.data?.status === true) {
            var data = getMasterReasonUnfollow.data?.output
              .data as MasterReasonUnFollow[];
            await setValue("masterReasonUnfollow", data);
            var ddl = data.map((a) => {
              return {
                value: a.id,
                labelTh: a.name,
                labelEn: a.name,
              } as OptionMultiLanguageProps;
            });
            setDdlMasterReason(ddl);
            setValueModal("caseId", caseId);
          } else {
            toast.error(getMessage(getMasterReasonUnfollow));
            setIsOpenPopup(false);
          }
        } else {
          toast.error(getMessage(getMasterReasonUnfollow));
          setIsOpenPopup(false);
        }
      });
    } else if (btnName === "addSaleComment") {
      setModalType("addSaleComment");
      setValueModal("caseId", caseId);
      setValueModal("updateRemarkSale.remark", data.saleRecord);
    } else if (btnName === "addData") {
      setModalType("addData");
      setValueModal("caseId", caseId);
      setValueModal(
        "updateReceiveServiceDate.receiveServiceDate",
        data.receiveServiceDate
      );
    } else if (btnName === "showDetail") {
      setModalType("showDetail");
      setValueModal("caseDetail", data);
    }
  };

  const showPopupAddClient = async() =>{
    setIsOpenPopupAddClient(true)
  }

  useEffect(() => {

    setIsLoading(true);
    var gettingData = true;
    if (gettingData) {
      onLoadData();
      loadCountCase().then(() => {
        setIsLoading(false);

        setChart({
          ...chartOption,
          series: [
            {
              name: "ยอดการติดต่อ",
              data: watch().countCase?.countContract,
            },
            {
              name: "ยอดเข้ารับการรักษา",
              data: watch().countCase?.countHealing,
            },
          ],
          yaxis: {
            title: {
              text: "จำนวนผู้เข้ารับบริการ",
            },
            min: 0,
            max: watch().countCase?.maxValueChart,
          },
        });
      });
    }
    return () => {
      gettingData = false;
    };
  }, []);

  return (

    <>
      <FormProvider {...methodsModal}>
        <Modal
          isOpen={isOpenPopup}
          toggle={onClosePopup}
          backdrop={"static"}
          keyboard={false}
        >
          <ModalHeader toggle={onClosePopup}>
            {modalType === "unfollow"
              ? "ยกเลิกติดตาม"
              : modalType === "addSaleComment"
                ? "บันทึกเพิ่มเติม"
                : modalType === "addData"
                  ? "เพิ่มข้อมูล / ระบุวันนัดหมาย"
                  : modalType === "showDetail"
                    ? "รายละเอียด"
                    : null}
          </ModalHeader>
          <ModalBody>
            {modalType === "unfollow" ? (
              <div className="row">
                <div className="col-12">
                  <InputFieldComponent
                    name="unfollowRequest.masterUnFollowId"
                    label="เหตุผลการเลิกติดตาม"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Select
                        {...field}
                        options={ddlMasterReason.map((a) => {
                          return {
                            value: a.value,
                            label: a.labelTh,
                          } as OptionProps;
                        })}
                        onChange={(e) => {
                          const selected = e as OptionProps;
                          resetFieldModal("unfollowRequest.masterUnFollowId");
                          setValueModal(
                            "unfollowRequest.masterUnFollowId",
                            selected.value
                          );
                        }}
                        value={
                          ddlMasterReason
                            .filter((a) => a.value === field.value)
                            ?.map((ct) => {
                              return {
                                value: ct.value,
                                label: ct.labelTh,
                              } as OptionProps;
                            }) || null
                        }
                        isSearchable={true}
                      />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="unfollowRequest.remarkUnfollow"
                    label="หมายเหตุ"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input
                        type={"textarea"}
                        {...field}
                        autoComplete="off"
                        rows="5"
                      />
                    )}
                  ></InputFieldComponent>
                </div>
              </div>
            ) : modalType === "addSaleComment" ? (
              <div className="row">
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="updateRemarkSale.remark"
                    label="หมายเหตุ"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input
                        type={"textarea"}
                        {...field}
                        autoComplete="off"
                        rows="5"
                      />
                    )}
                  ></InputFieldComponent>
                </div>
              </div>
            ) : modalType === "addData" ? (
              <div className="row">
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="updateReceiveServiceDate.receiveServiceDate"
                    label={"วันนัดหมาย"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <DatePicker
                        {...field}
                        // Select current date if no value, otherwise use the field value with timezone
                        selected={field.value ? new Date() : new Date()}
                        value={
                          field.value
                            ? moment.tz(field.value, "Asia/Bangkok").format("DD/MM/YYYY")
                            : moment.tz(new Date(), "Asia/Bangkok").format("DD/MM/YYYY") // Format current date
                        }
                        showIcon
                        dateFormat="dd/MM/yyyy" // Ensure the date is shown as dd/MM/yyyy
                        className="form-control"
                        wrapperClassName="w-100" // Full width wrapper
                        icon={<FaCalendarAlt />} // Calendar icon
                        autoComplete="off" // Disable browser auto-complete
                      />


                    )}
                  ></InputFieldComponent>
                </div>
              </div>
            ) : modalType === "showDetail" ? (
              <div className="row">
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.lastMonthlyPeriod"
                    label={"ประจำเดือนครั้งล่าสุด"}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <DatePicker
                        {...field}
                        selected={field.value ? field.value : null}
                        value={
                          field.value
                            ? moment
                              .tz(field.value, "Asia/Bangkok")
                              .format("DD/MM/YYYY")
                            : undefined
                        }
                        showIcon
                        dateFormat="dd/MM/yyyy"
                        className="form-control"
                        wrapperClassName="w-100"
                        icon={<FaCalendarAlt />}
                        autoComplete="off"
                        disabled={true}
                      />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.gestationalAge"
                    label="อายุครรภ์(สัปดาห์)"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input type={"text"} value={field.value === 0 ? "N/A" : field.value} disabled={true} />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.historyOfCesareanSection"
                    label="ประวัติผ่าคลอด"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input type={"text"} {...field} disabled={true} />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.marriedOrBoyfriend"
                    label="การแต่งงาน / มีแฟน หรือไม่"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input type={"text"} {...field} disabled={true} />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.drugAllergy"
                    label="ประวัติการแพ้ยา"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input type={"text"} {...field} disabled={true} />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.congenitalDisease"
                    label="โรคประจำตัว"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input type={"text"} {...field} disabled={true} />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.reasonTermination"
                    label="เหตุผลในการยุติ"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input type={"text"} {...field} disabled={true} />
                    )}
                  ></InputFieldComponent>
                </div>
                <div className="col-sm-12">
                  <InputFieldComponent
                    name="caseDetail.informationToDoctor"
                    label="ข้อมูลเพิ่มเติมถึงแพทย์"
                    require={true}
                    mode={InputMode.secondary}
                    labelAlignClassName="text-left"
                    labelClassName="col-sm-12"
                    inputClassName="col-sm-12"
                    renderControl={(field) => (
                      <Input
                        type={"textarea"}
                        {...field}
                        autoComplete="off"
                        rows="5"
                        disabled={true}
                      />
                    )}
                  ></InputFieldComponent>
                </div>
              </div>
            ) : null}
          </ModalBody>
          <ModalFooter>
            {modalType !== "" ? (
              <>
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={onClosePopup}
                >
                  ยกเลิก
                </button>
                {modalType !== "showDetail" ? (
                  <button
                    type="button"
                    className="btn btn-primary"
                    onClick={async () => {
                      const isValid = await triggerModal();
                      if (isValid) {
                        onSaveModal(watchModal());
                      }
                    }}
                  >
                    ยืนยัน
                  </button>
                ) : null}
              </>
            ) : null}
          </ModalFooter>
          <JsonViewerComponent data={watchModal()}></JsonViewerComponent>
        </Modal>
      </FormProvider>

      <FormProvider {...methods}>
        {isLoading ? (
          <Spinner />
        ) : (
          <>
            <div>
              <div className="row mb-3">

                <div className="col-md-3 col-sm-12 pt-2">
                  <div className="card border-5 border-primary border-start-0 border-end-0 border-top-0 shadow-sm">
                    <div className="card-body">
                      <h6 className="card-subtitle mb-2 text-muted">
                        ยอดเข้ารับบริการของคุณเดือนนี้
                      </h6>
                      <h2 className="card-title mb-0" style={{ textAlign: "end" }}>
                        <span
                          className="card-text"
                          style={{ fontWeight: "bold" }}
                        >
                          {watch().countCase?.countCaseCureMonthBySale}
                        </span>{" "}
                      </h2>
                    </div>
                  </div>
                </div>

                <div className="col-md-3 col-sm-12 pt-2">
                  <div className="card border-5 border-success border-start-0 border-end-0 border-top-0 shadow-sm">
                    <div className="card-body">
                      <h6 className="card-subtitle mb-2 text-muted">
                        ยอดเข้ารับบริการของทีมเดือนนี้
                      </h6>
                      <h2 className="card-title mb-0" style={{ textAlign: "end" }}>
                        <span
                          className="card-text"
                          style={{ fontWeight: "bold" }}
                        >
                          {watch().countCase?.countCaseCureMonth}
                        </span>{" "}
                      </h2>
                    </div>
                  </div>
                </div>

                <div className="col-md-3 col-sm-12 pt-2">
                  <div className="card border-5 border-warning border-start-0 border-end-0 border-top-0 shadow-sm">
                    <div className="card-body">
                      <h6 className="card-subtitle mb-2 text-muted">
                        ยอดการนัดหมายของคุณปีนี้
                      </h6>
                      <h2 className="card-title mb-0" style={{ textAlign: "end" }}>
                        <span
                          className="card-text"
                          style={{ fontWeight: "bold" }}
                        >
                          {watch().countCase?.countCaseYearBySale}
                        </span>{" "}
                      </h2>
                    </div>
                  </div>
                </div>

                <div className="col-md-3 col-sm-12 pt-2">
                  <div className="card border-5 border-danger border-start-0 border-end-0 border-top-0 shadow-sm">
                    <div className="card-body">
                      <h6 className="card-subtitle mb-2 text-muted">
                        ยอดการนัดหมายของทีมปีนี้
                      </h6>
                      <h2 className="card-title mb-0" style={{ textAlign: "end" }}>
                        <span
                          className="card-text"
                          style={{ fontWeight: "bold" }}
                        >
                          {watch().countCase?.countCaseYear}
                        </span>{" "}
                      </h2>
                    </div>
                  </div>
                </div>



              </div>

              <div className="row">
                <div className="col-12">
                  <div className="card border-0 shadow-sm">
                    <div className="card-body">
                      <div className="row justify-content-between">
                        <h5>
                          กราฟเปรียบเทียบยอดการนัดหมายและยอดเข้ารับบริการของคุณ
                        </h5>

                        <div className="col-12">
                          <div>
                            <Chart
                              options={chart}
                              series={chart.series}
                              type="line"
                              height={350}
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <br />
              <br />
              <br />
              <div className="row">

                <div className="col-sm-12 col-md-6 col-lg-7 pt-3">
                  <h6>
                    รายชื่อการนัดหมายรอเข้ารับบริการของทีม(
                    {watch().caseForSale?.countCaseFilter})
                  </h6>
                </div>



                <div className="col-sm-12 col-md-6 col-lg-5">
                  <div className="d-flex align-items-end" style={{ float: 'right' }}>
                    <button
                      type="button" title="Clipboard"
                      className="btn btn-success me-2 mb-3" // Add margin to the right for spacing
                      onClick={showPopupAddClient}
                    >
                      <BsBuildingAdd /> เพิ่มคนไข้
                    </button>
                    <button
                      type="button" title="Clipboard"
                      className="btn btn-primary me-2 mb-3" // Add margin to the right for spacing
                      onClick={copyToClipboard}
                    >
                      <BsCopy /> Your ref URL
                    </button>

                    <InputFieldComponent
                      name="textSearch"
                      mode={InputMode.secondary}
                      labelAlignClassName="text-left"
                      labelClassName="d-none" // Hide the label to keep it inline
                      inputClassName="flex-grow-1" // Allow the input to take remaining space
                      renderControl={(field) => (
                        <Input
                          type="text"
                          {...field}
                          className="form-control"
                          autoComplete="off"
                          placeholder="Search"
                          onKeyDown={(e) => {
                            if (e.key === "Enter") {
                              e.preventDefault(); // Prevent default form submission
                              onSearch(); // Call the onSearch function
                            }
                          }}
                        />
                      )}
                    />
                  </div>

                </div>
              </div>
              <div className="row">
                <div className="col-12">
                  <div className="card border-0 shadow-sm">
                    <div className="card-body">
                      <div className="row">
                        <div className="col-12">
                          <div className="table-responsive"> {/* Added this wrapper for responsiveness */}
                            <BootstrapTable
                              keyField="id"
                              data={watch().caseForSale?.caseDetailData || []}
                              hover
                              condensed
                              remote
                              noDataIndication={() => (
                                <div className="text-center">ไม่มีข้อมูล</div>
                              )}
                              headerClasses="text-center"
                              pagination={paginationFactory(paginationOptions)}
                              columns={columns}
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </>
        )}
        <JsonViewerComponent data={watch()}></JsonViewerComponent>
        <JsonViewerComponent data={errors}></JsonViewerComponent>
      </FormProvider>

      <Modal
          isOpen={isOpenPopupAddClient}
          toggle={onClosePopupAddClient}
          backdrop={"static"}
          keyboard={false}
        >
          <ModalHeader toggle={onClosePopupAddClient}>
            เพิ่มคนไข้
          </ModalHeader>
          <ModalBody>
           <ModalAddClient isOpenPopupAddClient={isOpenPopupAddClient} setIsOpenPopupAddClient={setIsOpenPopupAddClient}/>
          </ModalBody>
          <ModalFooter>
            {modalType !== "" ? (
              <>
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={onClosePopup}
                >
                  ยกเลิก
                </button>
                {modalType !== "showDetail" ? (
                  <button
                    type="button"
                    className="btn btn-primary"
                    onClick={async () => {
                      const isValid = await triggerModal();
                      if (isValid) {
                        onSaveModal(watchModal());
                      }
                    }}
                  >
                    ยืนยัน
                  </button>
                ) : null}
              </>
            ) : null}
          </ModalFooter>
          <JsonViewerComponent data={watchModal()}></JsonViewerComponent>
        </Modal>

    </>
  );
};

export default Home;
