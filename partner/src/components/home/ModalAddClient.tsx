import React, { useEffect, useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useHomeState } from "../../_store/Home";
import {
    HomeFromProps,
    FindWithPhoneRequest,
    FindWithPhoneResponse,
    MasterRefCode,
    CreateTransClientRequest,
} from "../../_types/Home";
import Spinner from "../../components/spinners/Spinner";
import { useAuth } from "../../context/UseAuth";
import { useHelper } from "../../context/Helper";
import InputFieldComponent, {
    InputMode,
} from "../../components/common/InputField";
import { toast } from "react-toastify";
import JsonViewerComponent from "../../components/common/JsonViewerComponent";
import DatePicker from "react-datepicker";
import moment from "moment-timezone";
import { FaCalendarAlt } from "react-icons/fa";
import Select from "react-select";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useNavigate } from "react-router-dom";
import {
    Input,
} from "reactstrap";

const validationRequestRegisterData = () => {
    return yup.object<HomeFromProps>({
        findWithPhoneRequest: yup.object<FindWithPhoneRequest>({
            phone: yup.string().required("กรุณาระบุข้อมูล"),
        }),
    });
};
const MySwal = withReactContent(Swal);

const ModalAddClient = ({ isOpenPopupAddClient, setIsOpenPopupAddClient }: { isOpenPopupAddClient: boolean, setIsOpenPopupAddClient: React.Dispatch<React.SetStateAction<boolean>> }) => {
    const isLoading = useHomeState((state) => state.isLoading);
    const setIsLoading = useHomeState((state) => state.setIsLoading);
    const verifyTelephone = useHomeState((state) => state.verifyTelephone);
    const verifyTelephoneWithCaseRegister = useHomeState((state) => state.verifyTelephoneWithCaseRegister);
    const { getMessage } = useHelper();
    const { profile } = useAuth();
    const [isCreateClient, setIsCreateClient] = useState(false);
    const getTransSaleGroup = useHomeState((state) => state.getTransSaleGroup);
    const createTransClient = useHomeState((state) => state.createTransClient);
    const [ddlRefCode, setddlRefCode] = useState<
        OptionMultiLanguageProps[]
    >([]);

    const navigate = useNavigate();

    const methods = useForm<HomeFromProps>({
        mode: "onChange",
        defaultValues: undefined,
        resolver: yupResolver(validationRequestRegisterData()),
    });
    const {
        formState: { errors },
        setValue,
        watch,
        trigger,
        resetField: resetFieldModal,
    } = methods;

    const verifyphone = async () => {
        setIsLoading(true);
        const data = {
            code: "+66",
            phone: watch().findWithPhoneRequest?.phone
        } as FindWithPhoneRequest | undefined
        await verifyTelephoneWithCaseRegister(data).then(
            (res: ResponseProps) => {
                if (res.data?.code === 200) {
                    if (res.data?.status === true) {
                        var response = res.data.output.data as FindWithPhoneResponse;
                        setIsCreateClient(true)
                        if (response.isDuplicate) {
                            setValue("createTransClientRequest.fullName", response.fullName)
                            setValue("createTransClientRequest.dateOfBirth", response.dateOfBirth)
                        }
                        setIsLoading(false);
                    }
                    else {
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

    const getDropdown = async () => {
        const masterSaleGroupId = profile().masterSaleGroupId !== undefined ? profile().masterSaleGroupId?.toString() : ""
        await getTransSaleGroup(masterSaleGroupId).then(async (res) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    var data = res.data?.output
                        .data as MasterRefCode[];
                    await setValue("masterRefCode", data);
                    var ddl = data.map((a) => {
                        return {
                            value: a.refCode,
                            labelTh: a.fullName,
                            labelEn: a.fullName,
                        } as OptionMultiLanguageProps;
                    });
                    setddlRefCode(ddl);
                } else {
                    toast.error(getMessage(res));
                }
            } else {
                toast.error(getMessage(res));
            }
        });
    }

    const createClient = async () => {
        const data = {
            dateOfBirth: watch().createTransClientRequest?.dateOfBirth ? moment.tz(watch().createTransClientRequest?.dateOfBirth, "Asia/Bangkok").format("YYYY-MM-DD") : undefined,
            fullName: watch().createTransClientRequest?.fullName,
            gestationalAge: watch().createTransClientRequest?.gestationalAge,
            receiveServiceDate: watch().createTransClientRequest?.receiveServiceDate ? moment.tz(watch().createTransClientRequest?.receiveServiceDate, "Asia/Bangkok").format("YYYY-MM-DD") : undefined,
            saleRecord: watch().createTransClientRequest?.saleRecord,
            telephoneNumber: watch().findWithPhoneRequest?.phone,
            tranSaleRefCode: watch().createTransClientRequest?.tranSaleRefCode
        } as CreateTransClientRequest

        await createTransClient(data).then(async (res) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    var response = res.data.output.data as any;
                    MySwal.fire({
                        icon: "success",
                        text: `บันทึกสำเร็จ`,
                        confirmButtonText: "ตกลง",
                        confirmButtonColor: "#3085d6",
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then(async (result) => {
                        setIsOpenPopupAddClient(false);
                        navigate(0);
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
    }

    useEffect(() => {
        getDropdown().then();
    }, []);

    return (

        <>
            <FormProvider {...methods}>
                {isLoading ? (
                    <Spinner />
                ) : (
                    <>
                        <div className="row">
                            <div className="col-9">
                                <InputFieldComponent
                                    name="findWithPhoneRequest.phone"
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
                                            placeholder="เบอร์โทร"
                                            maxLength={15}
                                        />
                                    )}
                                />
                            </div>
                            <div className="col-3">
                                <button
                                    type="button" title="Clipboard"
                                    className="btn btn-primary me-2 mb-3" // Add margin to the right for spacing
                                    onClick={async () => {
                                        const isValid = await trigger();
                                        if (isValid) {
                                            verifyphone();
                                        }
                                    }}
                                >
                                    ตรวจสอบ
                                </button>
                            </div>
                        </div>
                        <br />
                        {
                            isCreateClient ?
                                <>
                                    <div className="row">
                                        <div className="col-12">
                                            <InputFieldComponent
                                                name="createTransClientRequest.fullName"
                                                mode={InputMode.secondary}
                                                label="ชื่อ - นามสกุล"
                                                labelAlignClassName="text-left"
                                                labelClassName="d-none" // Hide the label to keep it inline
                                                inputClassName="flex-grow-1" // Allow the input to take remaining space
                                                renderControl={(field) => (
                                                    <Input
                                                        type="text"
                                                        {...field}
                                                        className="form-control"
                                                        autoComplete="off"
                                                        placeholder="ชื่อ - สกุล"
                                                        maxLength={255}
                                                    />
                                                )}
                                            />
                                        </div>
                                        <div className="col-12">
                                            <InputFieldComponent
                                                name="createTransClientRequest.dateOfBirth"
                                                label="วัน/เดือน/ปี เกิด"
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="col-sm-12"
                                                inputClassName="col-sm-12"
                                                renderControl={field => (
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
                                                        maxDate={new Date()}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        <div className="col-12">
                                            <InputFieldComponent
                                                name="createTransClientRequest.gestationalAge"
                                                mode={InputMode.secondary}
                                                label="อายุครรภ์"
                                                labelAlignClassName="text-left"
                                                labelClassName="d-none" // Hide the label to keep it inline
                                                inputClassName="flex-grow-1" // Allow the input to take remaining space
                                                renderControl={(field) => (
                                                    <Input
                                                        type="number"
                                                        {...field}
                                                        className="form-control"
                                                        autoComplete="off"
                                                        placeholder="อายุครรภ์"
                                                    />
                                                )}
                                            />
                                        </div>
                                        <div className="col-12">
                                            <InputFieldComponent
                                                name="createTransClientRequest.saleRecord"
                                                mode={InputMode.secondary}
                                                label="บันทึกทีมขาย"
                                                labelAlignClassName="text-left"
                                                labelClassName="d-none" // Hide the label to keep it inline
                                                inputClassName="flex-grow-1" // Allow the input to take remaining space
                                                renderControl={(field) => (
                                                    <Input
                                                        type="text"
                                                        {...field}
                                                        className="form-control"
                                                        autoComplete="off"
                                                        placeholder="บันทึกทีมขาย"
                                                        maxLength={255}
                                                    />
                                                )}
                                            />
                                        </div>
                                        <div className="col-12">
                                            <InputFieldComponent
                                                name="createTransClientRequest.receiveServiceDate"
                                                label="วันที่เข้ารับบริการ"
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="col-sm-12"
                                                inputClassName="col-sm-12"
                                                renderControl={field => (
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
                                                        minDate={new Date()}
                                                    />
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                        <div className="col-12">
                                            <InputFieldComponent
                                                name="createTransClientRequest.tranSaleRefCode"
                                                label="ผู้แนะนำ"
                                                require={true}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="col-sm-12"
                                                inputClassName="col-sm-12"
                                                renderControl={(field) => (
                                                    <Select
                                                        {...field}
                                                        options={ddlRefCode.map((a) => {
                                                            return {
                                                                value: a.value,
                                                                label: a.labelTh,
                                                            } as OptionProps;
                                                        })}
                                                        onChange={(e) => {
                                                            const selected = e as OptionProps;
                                                            resetFieldModal("createTransClientRequest.tranSaleRefCode");
                                                            setValue(
                                                                "createTransClientRequest.tranSaleRefCode",
                                                                selected.value
                                                            );
                                                        }}
                                                        value={
                                                            ddlRefCode
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
                                    </div>

                                    <br />
                                    <div className="row">
                                        <div className="col-9"></div>
                                        <div className="col-3">
                                            <button
                                                type="button" title="Clipboard"
                                                className="btn btn-success me-2 mb-3" // Add margin to the right for spacing
                                                onClick={async () => {
                                                    const isValid = await trigger();
                                                    if (isValid) {
                                                        createClient()
                                                    }
                                                }}
                                            >
                                                บันทึก
                                            </button>
                                        </div>
                                    </div>
                                </>
                                : null
                        }
                    </>
                )}
                <JsonViewerComponent data={watch()}></JsonViewerComponent>
                <JsonViewerComponent data={errors}></JsonViewerComponent>
            </FormProvider>
        </>
    );
};

export default ModalAddClient;
