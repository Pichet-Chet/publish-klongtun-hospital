import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useAuth } from "../../context/UseAuth";
import { useChangeAppointmentState } from "../../_store/ChangeAppointment";
import { AppointmentRequestFormProps, TransCaseApiProps, CancelAppointmentRequestApiProps } from "../../_types/ChangeAppointment";
import { useHelper } from "../../context/Helper";
import InputFieldComponent, { InputMode } from "../../components/common/InputField";
import Spinner from "../../components/spinners/Spinner";
import { Card, CardBody, Row, Col } from "reactstrap";
import { Button, DatePicker, Message, useToaster } from 'rsuite';
import moment from "moment-timezone";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from "react-router-dom";
import { UserHaveCaesProps } from "../../_types/Client";

import JsonViewerComponent from "../../components/common/JsonViewerComponent";

const MySwal = withReactContent(Swal);
const messageError = (msg: String) => (
    <Message showIcon type={"error"} closable>
        <strong>แจ้งเตือน!</strong> {msg}
    </Message>
);
const ChangeAppointmentDetail = ({ transCase }: { transCase?: UserHaveCaesProps }) => {
    const { t } = useTranslation("changeappointment");
    const { t: tg } = useTranslation("global");
    const toaster = useToaster();
    const navigate = useNavigate();
    const { user, logout } = useAuth();
    const { getMessage } = useHelper();
    const isLoading = useChangeAppointmentState((state) => state.isLoading);
    const setIsLoading = useChangeAppointmentState((state) => state.setIsLoading);
    const requestCancelAppointment = useChangeAppointmentState((state) => state.requestCancelAppointment);
    const [postponeAppointment, setPostponeAppointment] = useState<boolean>(false);
    const validationPostponeAppointment = useChangeAppointmentState(
        (state) => state.validationPostponeAppointment
    );
    const getCaseById = useChangeAppointmentState((state) => state.getCaseById);
    const requestUpdateAppointment = useChangeAppointmentState((state) => state.requestUpdateAppointment);

    const methods = useForm<AppointmentRequestFormProps>({
        mode: "onChange",
        resolver: yupResolver(validationPostponeAppointment(t)),
        defaultValues: undefined,
    });
    const { formState: { errors }, getValues, watch, resetField, trigger } = methods;

    const onPostponeAppointment: SubmitHandler<AppointmentRequestFormProps> = async (data) => {
        setIsLoading(true);
        await getCaseById(transCase?.caseId || "").then(async (resCase) => {
            if (resCase.data?.code === 200) {
                if (resCase.data?.status === true) {
                    var dataCase = resCase.data.output.data as TransCaseApiProps;
                    var postData: TransCaseApiProps = {
                        ...dataCase,
                        receiveServiceDate: moment
                            .tz(data.appointmentData, "Asia/Bangkok")
                            .format("YYYY-MM-DD"),
                        updatedBy: user?.id,
                        masterConsultRoomId: ""
                    };
                    await requestUpdateAppointment(postData).then((resUpdate: ResponseProps) => {
                        if (resUpdate.data?.code === 200) {
                            if (resUpdate.data?.status === true) {
                                MySwal.fire({
                                    icon: "success",
                                    text: `${t("appointmentDetail.alert.postponeAppointmentTitle")}`,
                                    confirmButtonText: `${t("appointmentDetail.btn.btnOk")}`,
                                    confirmButtonColor: "#3085d6",
                                    allowOutsideClick: false,
                                    allowEscapeKey: false,
                                }).then((result) => {
                                    navigate("/");
                                });
                            } else {
                                toaster.push(messageError(getMessage(resUpdate)), { duration: 5000 });
                            }
                        } else {
                            toaster.push(messageError(getMessage(resUpdate)), { duration: 5000 });
                        }
                    });
                } else {
                    toaster.push(messageError(getMessage(resCase)), { duration: 5000 });
                }
            } else {
                toaster.push(messageError(getMessage(resCase)), { duration: 5000 });
            }
        });
    };

    const onCancelAppointment = () => {
        const dataCancel: CancelAppointmentRequestApiProps = {
            id: transCase?.caseId,
            statusCode: 'CS-01'
        };
        requestCancelAppointment(dataCancel).then((resCancel) => {
            if (resCancel.data?.code === 200) {
                if (resCancel.data?.status === true) {
                    MySwal.fire({
                        icon: "success",
                        text: `${t("appointmentDetail.alert.cancelAppointmentTitle")}`,
                        confirmButtonText: `${t("appointmentDetail.btn.btnOk")}`,
                        confirmButtonColor: "#3085d6",
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then((result) => {
                        navigate("/");
                    });
                } else {
                    toaster.push(messageError(getMessage(resCancel)), { duration: 5000 });
                }
            } else {
                toaster.push(messageError(getMessage(resCancel)), { duration: 5000 });
            }
        });
    }

    return (
        <FormProvider {...methods}>
            {isLoading ? (
                <Spinner />
            ) : (
                <>
                    <div className="row d-flex align-items-center">
                        <div className="col-sm-6">
                            <h6 className="card-title mb-2" style={{ fontSize: "18px" }}>
                                {`${t("title")} ${user?.fullName}`}
                            </h6>
                        </div>
                        <div className="col-sm-6">
                            <div className="d-flex justify-content-end">
                                <Button
                                    color="red"
                                    appearance="primary"
                                    onClick={async () => {
                                        logout();
                                    }}
                                >
                                    {tg("logout")}
                                </Button>
                            </div>
                        </div>
                    </div>
                    <Card className="mt-2 mb-3">
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="viewReceiveServiceDate"
                                        label={t("appointmentDetail.form.receiveServiceDate")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <DatePicker
                                                {...field}
                                                format="dd/MM/yyyy"
                                                placeholder="DD/MM/YYYY"
                                                editable={false}
                                                value={transCase?.receiveServiceDate ? new Date(transCase?.receiveServiceDate) : null}
                                                className="w-100"
                                                disabled={true}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                    {
                        postponeAppointment === false ? (
                            <>
                                <Row className="mb-3">
                                    <Col xs={12} lg={4} className="mb-2">
                                        <Button
                                            appearance="default"
                                            className="w-100"
                                            onClick={async () => {
                                                await resetField("appointmentData", undefined);
                                                await setPostponeAppointment(false);
                                                navigate("/");
                                            }}
                                        >
                                            {t("appointmentDetail.btn.btnBack")}
                                        </Button>
                                    </Col>
                                    {
                                        transCase?.isNewAppointment === false && (
                                            <>
                                                <Col xs={12} lg={4} className="mb-2">
                                                    <Button
                                                        color="red"
                                                        appearance="primary"
                                                        className="w-100"
                                                        onClick={async () => {
                                                            MySwal.fire({
                                                                icon: 'question',
                                                                html: `${t("appointmentDetail.alert.cancelComfirmAppointmentTitle")}`,
                                                                showCancelButton: true,
                                                                confirmButtonText: `${t("appointmentDetail.btn.btnCancelAppointmentConfirm")}`,
                                                                cancelButtonText: `${t("appointmentDetail.btn.btnCancelAppointmentCancel")}`,
                                                                confirmButtonColor: '#3085d6',
                                                                cancelButtonColor: '#d33',
                                                                reverseButtons: true,
                                                            }).then(async (result) => {
                                                                if (result.isConfirmed) {
                                                                    onCancelAppointment();
                                                                }
                                                            });
                                                        }}
                                                    >
                                                        {t("appointmentDetail.btn.btnCancelAppointment")}
                                                    </Button>
                                                </Col>
                                                <Col xs={12} lg={4} className="mb-2">
                                                    <Button
                                                        color="green"
                                                        appearance="primary"
                                                        className="w-100"
                                                        onClick={async () => {
                                                            setPostponeAppointment(true);
                                                        }}
                                                    >
                                                        {t("appointmentDetail.btn.btnPostponeAppointment")}
                                                    </Button>
                                                </Col>
                                            </>
                                        )
                                    }
                                </Row>
                            </>
                        ) : (
                            <>
                                <Card className="mt-3 mb-3">
                                    <CardBody>
                                        <Row>
                                            <Col sm={12}>
                                                <InputFieldComponent
                                                    name="appointmentData"
                                                    label={t("appointmentDetail.form.appointmentData")}
                                                    mode={InputMode.secondary}
                                                    labelAlignClassName="text-left"
                                                    labelClassName="col-sm-12"
                                                    inputClassName="col-sm-12"
                                                    renderControl={(field) => (
                                                        <DatePicker
                                                            {...field}
                                                            format="dd/MM/yyyy"
                                                            placeholder="DD/MM/YYYY"
                                                            editable={false}
                                                            className="w-100"
                                                            shouldDisableDate={(date: Date) => {
                                                                const today = new Date();
                                                                const yesterday = new Date(today);
                                                                return date < new Date(yesterday.setDate(today.getDate() - 1));
                                                            }}
                                                            oneTap
                                                        />
                                                    )}
                                                ></InputFieldComponent>
                                            </Col>
                                        </Row>
                                    </CardBody>
                                </Card>
                                <Row className="mb-3">
                                    <Col xs={6} className="mb-2">
                                        <Button
                                            color="red"
                                            appearance="primary"
                                            className="w-100"
                                            onClick={async () => {
                                                await resetField("appointmentData", undefined);
                                                await setPostponeAppointment(false);
                                            }}
                                        >
                                            {t("appointmentDetail.btn.btnCancel")}
                                        </Button>
                                    </Col>
                                    <Col xs={6} className="mb-2">
                                        <Button
                                            color="green"
                                            appearance="primary"
                                            className="w-100"
                                            onClick={async () => {
                                                const isValid = await trigger(); // ตรวจสอบ validation ก่อน
                                                if (isValid) {
                                                    MySwal.fire({
                                                        icon: 'question',
                                                        html: `${t("appointmentDetail.alert.postponeComfirmAppointmentTitle")} ${getValues().appointmentData &&
                                                            moment.tz(getValues().appointmentData, "Asia/Bangkok").format("DD/MM/YYYY")}`,
                                                        showCancelButton: true,
                                                        confirmButtonText: `${t("appointmentDetail.btn.btnPostponeAppointmentConfirm")}`,
                                                        cancelButtonText: `${t("appointmentDetail.btn.btnPostponeAppointmentEdit")}`,
                                                        confirmButtonColor: '#3085d6',
                                                        cancelButtonColor: '#d33',
                                                        reverseButtons: true,
                                                        allowOutsideClick: false,
                                                        allowEscapeKey: false,
                                                    }).then(async (result) => {
                                                        if (result.isConfirmed) {
                                                            onPostponeAppointment(getValues()); // เรียก onSubmit ด้วยค่าที่ถูกต้อง
                                                        }
                                                    });
                                                }
                                            }}
                                        >
                                            {t("appointmentDetail.btn.btnOk")}
                                        </Button>
                                    </Col>
                                </Row>
                            </>
                        )
                    }
                </>
            )}
            <JsonViewerComponent data={watch()}></JsonViewerComponent>
            <JsonViewerComponent data={errors}></JsonViewerComponent>
        </FormProvider>
    );
};

export default ChangeAppointmentDetail;
