import React, { useState, useEffect } from "react";
import { useTranslation } from 'react-i18next';
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useMasterState } from "../../_store/Master";
import { useLoginState } from "../../_store/Login";
import { LoginFromProps } from "../../_types/Login";
import { AuthProfileProps } from "../../_types/Client";
import { MasterCountryProps } from "../../_types/Master";
import InputFieldComponent, { InputMode } from "../../components/common/InputField";
import Spinner from "../../components/spinners/Spinner";
import { useAuth } from "../../context/UseAuth";
import { useHelper } from "../../context/Helper";
import { Input, Button, SelectPicker, DatePicker, Message, useToaster } from 'rsuite';
import moment from 'moment-timezone';
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import { useNavigate } from "react-router-dom";
import CustomNavbar from "../../components/navbar/Navbar";
import CustomFooter from "../../components/footer/Footer";

import JsonViewerComponent from "../../components/common/JsonViewerComponent";

const MySwal = withReactContent(Swal);
const messageError = (msg: String) => (
    <Message showIcon type={"error"} closable>
        <strong>แจ้งเตือน!</strong> {msg}
    </Message>
);
const Verification = (props: any) => {
    const { t, i18n } = useTranslation("login");
    const toaster = useToaster();
    const navigate = useNavigate();
    const { user, loginUser } = useAuth();
    const { getMessage } = useHelper();
    const isLoading = useLoginState((state) => state.isLoading);
    const setIsLoading = useLoginState((state) => state.setIsLoading);
    const validationData = useLoginState((state) => state.validationData);
    const clientLogin = useLoginState((state) => state.clientLogin);

    const methods = useForm<LoginFromProps>({
        mode: "onChange",
        resolver: yupResolver(validationData(t)),
        defaultValues: {
            telephoneNumber: '',
            dateOfBirth: undefined,
        },
    });
    const { formState: { errors }, getValues, watch, trigger } = methods;

    const onSubmit: SubmitHandler<LoginFromProps> = async (data) => {
        setIsLoading(true);
        await clientLogin(
            data.telephoneNumber,
            moment.tz(data.dateOfBirth, "Asia/Bangkok").format("YYYY-MM-DD"),
            "+66"
        ).then(async (res: ResponseProps) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    await loginUser(res);
                    const profile = JSON.parse(localStorage.getItem("profile") || "") as AuthProfileProps;
                    toaster.push(<Message showIcon type={"success"} closable>{`${t("alert.loginSuccess")} ${profile.fullName}`}</Message>, { duration: 5000 });
                    navigate('/changeappointment');
                } else {
                    MySwal.fire({
                        icon: 'error',
                        text: `${t("alert.loginTitle")}`,
                        showCancelButton: true,
                        confirmButtonText: `${t("btn.btnReCheck")}`,
                        cancelButtonText: `${t("btn.btnCancel")}`,
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
                    });
                }
            } else {
                toaster.push(messageError(getMessage(res)), { duration: 5000 });
            }
            setIsLoading(false);
        }).catch(error => {
            toaster.push(messageError(JSON.stringify(error)), { duration: 5000 });
            setIsLoading(false);
        });
    };

    useEffect(() => {
        setIsLoading(true);
        var gettingData = true;
        if (gettingData) {
            setIsLoading(false);
        }
        return () => { gettingData = false };
    }, []);

    return (
        <FormProvider {...methods}>
            {
                isLoading ? <Spinner />
                    :
                    <>
                        <CustomNavbar />
                        <div className="container-fluid d-flex flex-column p-0" style={{ minHeight: 'calc(100vh)', backgroundColor: "#e3e3e3" }}>
                            <div className="p-3">
                                <div className="row d-flex justify-content-center" style={{ marginTop: "100px", marginBottom: "90px" }}>
                                    <div className="col-md-6">
                                        <div className="card mb-3">
                                            <div className="card-body">
                                                <div className="text-center mb-3 mt-4">
                                                    <h5 className="text-muted mb-4">
                                                        {t("title")}
                                                    </h5>
                                                </div>
                                                <div className="mb-3 mt-4">
                                                    <InputFieldComponent
                                                        name="telephoneNumber"
                                                        require={true}
                                                        label={t("form.telephoneNumber")}
                                                        mode={InputMode.secondary}
                                                        labelAlignClassName="text-left"
                                                        labelClassName="col-sm-12"
                                                        inputClassName="col-sm-12"
                                                        renderControl={field => (
                                                            <Input
                                                                type={'tel'}
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
                                                        label={t("form.dateOfBirth")}
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
                                                                navigate('/');
                                                            }}>
                                                            {t("btn.btnCancel")}
                                                        </Button>
                                                    </div>
                                                    <div className="col-6">
                                                        <Button
                                                            color="green"
                                                            appearance="primary"
                                                            className="w-100"
                                                            onClick={async () => {
                                                                const isValid = await trigger();  // ตรวจสอบ validation ก่อน
                                                                if (isValid) {
                                                                    onSubmit(getValues());  // เรียก onSubmit ด้วยค่าที่ถูกต้อง
                                                                }
                                                            }}
                                                        >
                                                            {t("btn.btnLogin")}
                                                        </Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <CustomFooter />
                        </div>
                    </>
            }
            {/* <JsonViewerComponent data={watch()}></JsonViewerComponent>
            <JsonViewerComponent data={errors}></JsonViewerComponent> */}
        </FormProvider>
    );
};

export default Verification;