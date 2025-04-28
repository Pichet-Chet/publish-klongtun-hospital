import React, { useEffect } from "react";
import { useTranslation } from "react-i18next";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import "./RegisterCheckData.css";
import { useHelper } from "../../context/Helper";
import { useRegisterState } from "../../_store/Register";
import InputFieldComponent, { InputMode, } from "../../components/common/InputField";
import Spinner from "../../components/spinners/Spinner";
import { Input, Button, DatePicker, Message, useToaster } from 'rsuite';
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from "react-router-dom";
import { RegisterUserFromProps, FindWithPhoneProps } from "../../_types/Register";
import moment from "moment-timezone";

import JsonViewerComponent from "../../components/common/JsonViewerComponent";

const MySwal = withReactContent(Swal);
const messageError = (msg: String) => (
    <Message showIcon type={"error"} closable>
        <strong>แจ้งเตือน!</strong> {msg}
    </Message>
);
const RegisterCheckData = ({ isSetRegister }: { isSetRegister: (isRegister: boolean) => void }) => {
    const { t } = useTranslation("register");
    const toaster = useToaster();
    const navigate = useNavigate();
    const { getMessage } = useHelper();
    const isLoading = useRegisterState((state) => state.isLoading);
    const setIsLoading = useRegisterState((state) => state.setIsLoading);
    const validationRequestOtpData = useRegisterState((state) => state.validationRequestOtpData);
    const findWithPhone = useRegisterState((state) => state.findWithPhone);

    const methods = useForm<RegisterUserFromProps>({
        mode: "onChange",
        resolver: yupResolver(validationRequestOtpData(t)),
        defaultValues: undefined,
    });
    const { formState: { errors }, getValues, watch, setValue, reset, trigger } = methods;

    const onRequestCheckData: SubmitHandler<RegisterUserFromProps> = async (data) => {
        setIsLoading(true);
        await findWithPhone(data.telephoneNumber).then(async (res: ResponseProps) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    var dataFindWithPhone = res.data.output.data as FindWithPhoneProps;
                    var isDuplicate = dataFindWithPhone.isDuplicate;
                    if (isDuplicate === false) {
                        localStorage.setItem("check_phone", data.telephoneNumber || "");
                        localStorage.setItem("check_dateOfBirth", moment.tz(data.dateOfBirth, "Asia/Bangkok").format("YYYY-MM-DD") || "");
                        isSetRegister(true);
                        setIsLoading(false);
                    } else {
                        MySwal.fire({
                            icon: 'error',
                            text: `${t("registerOtp.alert.checkData")}`,
                            showCancelButton: true,
                            confirmButtonText: `${t("registerOtp.btn.btnReCheck")}`,
                            cancelButtonText: `${t("registerOtp.btn.btnCancel")}`,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            reverseButtons: true,
                            allowOutsideClick: false,
                            allowEscapeKey: false,
                        }).then((result) => {
                            if (result.isConfirmed) {
                            } else if (result.dismiss === Swal.DismissReason.cancel) {
                                navigate('/');
                            }
                            setIsLoading(false);
                        });
                    }
                } else {
                    MySwal.fire({
                        icon: 'error',
                        text: `${getMessage(res)}`,
                        showCancelButton: true,
                        confirmButtonText: `${t("registerOtp.btn.btnReCheck")}`,
                        cancelButtonText: `${t("registerOtp.btn.btnCancel")}`,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        reverseButtons: true,
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then((result) => {
                        if (result.isConfirmed) {
                        } else if (result.dismiss === Swal.DismissReason.cancel) {
                            navigate('/');
                        }
                        setIsLoading(false);
                    });
                }
            } else {
                toaster.push(messageError(getMessage(res)), { duration: 5000 });
                setIsLoading(false);
            }
        }).catch(error => {
            toaster.push(messageError(JSON.stringify(error)), { duration: 5000 });
            setIsLoading(false);
        });
    };
    const onCancel = () => {
        localStorage.removeItem("check_phone");
        localStorage.removeItem("check_dateOfBirth");
        var defaultData: RegisterUserFromProps = {
            telephoneNumber: "",
            dateOfBirth: undefined
        };
        reset({ ...defaultData });
        navigate("/register");
    };

    useEffect(() => {
        setIsLoading(true);
        var gettingData = true;
        if (gettingData) {
            setIsLoading(false);
        }
        return () => {
            gettingData = false;
        };
    }, []);

    return (
        <FormProvider {...methods}>
            {isLoading ? (
                <Spinner />
            ) : (
                <>
                    <div className="card mb-3">
                        <div className="card-body">
                            <div className="mb-3 mt-4">
                                <InputFieldComponent
                                    name="telephoneNumber"
                                    require={true}
                                    label={t("registerOtp.form.telephoneNumber")}
                                    mode={InputMode.secondary}
                                    labelAlignClassName="text-left"
                                    labelClassName="col-sm-12"
                                    inputClassName="col-sm-12"
                                    renderControl={(field) => (
                                        <Input
                                            type={"tel"}
                                            {...field}
                                            maxLength={15}
                                            autoComplete="off"
                                        />
                                    )}
                                ></InputFieldComponent>
                            </div>
                            <div className="mb-3">
                                <InputFieldComponent
                                    name="dateOfBirth"
                                    require={true}
                                    label={t("registerOtp.form.dateOfBirth")}
                                    mode={InputMode.secondary}
                                    labelAlignClassName="text-left"
                                    labelClassName="col-sm-12"
                                    inputClassName="col-sm-12"
                                    renderControl={field => (
                                        <DatePicker
                                            {...field}
                                            format="dd/MM/yyyy"
                                            placeholder="DD/MM/YYYY"
                                            editable={false}
                                            className="w-100"
                                            shouldDisableDate={(date: Date) => date > new Date()}
                                            oneTap
                                        />
                                    )}
                                ></InputFieldComponent>
                            </div>
                            <div className="row">
                                <div className="col-6">
                                    <Button
                                        color="red"
                                        appearance="primary"
                                        className="w-100"
                                        onClick={() => {
                                            onCancel();
                                            navigate("/");
                                        }}
                                    >
                                        {t("registerOtp.btn.btnCancel")}
                                    </Button>
                                </div>
                                <div className="col-6">
                                    <Button
                                        color="green"
                                        appearance="primary"
                                        className="w-100"
                                        onClick={async () => {
                                            const isValid = await trigger();
                                            if (isValid) {
                                                onRequestCheckData(getValues());
                                            }
                                        }}
                                    >
                                        {t("registerOtp.btn.btnRequestCheckPhone")}
                                    </Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </>
            )}
            <JsonViewerComponent data={watch()}></JsonViewerComponent>
            <JsonViewerComponent data={errors}></JsonViewerComponent>
        </FormProvider>
    );
};

export default RegisterCheckData;
