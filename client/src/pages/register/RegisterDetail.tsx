import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useHelper } from "../../context/Helper";
import { useMasterState } from "../../_store/Master";
import { useRegisterState } from "../../_store/Register";
import { RegisterUserRequestFormProps, RegisterUserRequestApiProps } from "../../_types/Register";
import InputFieldComponent, { InputMode } from "../../components/common/InputField";
import Spinner from "../../components/spinners/Spinner";
import { Card, CardHeader, CardBody, CardTitle, Row, Col } from "reactstrap";
import { Input, Button, SelectPicker, DatePicker, Message, useToaster } from 'rsuite';
import moment from "moment-timezone";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from "react-router-dom";

import JsonViewerComponent from "../../components/common/JsonViewerComponent";

const MySwal = withReactContent(Swal);
const messageError = (msg: String) => (
    <Message showIcon type={"error"} closable>
        <strong>แจ้งเตือน!</strong> {msg}
    </Message>
);
const RegisterDetail = ({ refCodeName }: { refCodeName: string }) => {
    const { t, i18n } = useTranslation("register");
    const toaster = useToaster();
    const navigate = useNavigate();
    const { getMessage } = useHelper();
    const isLoading = useRegisterState((state) => state.isLoading);
    const setIsLoading = useRegisterState((state) => state.setIsLoading);
    const masterNationality = useMasterState((state) => state.masterNationality);
    const validationRequestRegisterData = useRegisterState(
        (state) => state.validationRequestRegisterData
    );
    const requestRegister = useRegisterState((state) => state.requestRegister);
    const maxLength = 256;
    const [nationalityOptions, setNationalityOptions] = useState<OptionMultiLanguageProps[]>([]);

    const methods = useForm<RegisterUserRequestFormProps>({
        mode: "onChange",
        resolver: yupResolver(validationRequestRegisterData(t)),
        defaultValues: undefined,
    });
    const {
        formState: { errors },
        getValues,
        watch,
        setValue,
        trigger,
    } = methods;

    const onSubmit: SubmitHandler<RegisterUserRequestFormProps> = async (data) => {
        setIsLoading(true);
        var postData: RegisterUserRequestApiProps = {
            tranSaleRefCode: data.tranSaleRefCode,
            fullName: data.fullName,
            citizenIdentification:
                i18n.language === "th" ? data.citizenIdentification : undefined,
            passportNumber: i18n.language === "en" ? data.passportNumber : undefined,
            address: data.address,
            dateOfBirth: data.dateOfBirth ? moment
                .tz(data.dateOfBirth, "Asia/Bangkok")
                .format("YYYY-MM-DD")
                : undefined,
            telephoneCode: "+66",
            telephoneNumber: data.telephoneNumber,
            masterNationalityId: data.masterNationality
                ? data.masterNationality
                : undefined,
            lastMonthlyPeriod: data.lastMonthlyPeriod ? moment
                .tz(data.lastMonthlyPeriod, "Asia/Bangkok")
                .format("YYYY-MM-DD")
                : undefined,
            gestationalAge: data.gestationalAge || 0,
            historyOfCesareanSection: data.historyOfCesareanSection
                ? data.historyOfCesareanSection === "no"
                    ? false
                    : true
                : undefined,
            marriedOrBoyfriend: data.marriedOrBoyfriend
                ? data.marriedOrBoyfriend === "no"
                    ? false
                    : true
                : undefined,
            drugAllergy: data.drugAllergy,
            congenitalDisease: data.congenitalDisease,
            reasonTermination: data.reasonTermination,
            informationToDoctor: data.informationToDoctor,
            receiveServiceDate: data.receiveServiceDate ? moment
                .tz(data.receiveServiceDate, "Asia/Bangkok")
                .format("YYYY-MM-DD")
                : undefined,
            createdBy: data.fullName,
        };
        await requestRegister(postData).then((res: ResponseProps) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    onRemoveLocalStorage();
                    MySwal.fire({
                        icon: 'success',
                        text: `${t("registerDetail.alert.submitSuccess")}`,
                        confirmButtonText: `${t("registerDetail.btn.btnConfirm")}`,
                        confirmButtonColor: '#3085d6',
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then(async (result) => {
                        navigate("/");
                    });
                } else {
                    setIsLoading(false);
                    toaster.push(messageError(getMessage(res)), { duration: 5000 });
                }
            } else {
                setIsLoading(false);
                toaster.push(messageError(getMessage(res)), { duration: 5000 });
            }
        });
    };

    const onRemoveLocalStorage = () => {
        localStorage.removeItem("check_phone");
        localStorage.removeItem("check_dateOfBirth");
    };

    useEffect(() => {
        var gettingData = true;
        if (gettingData) {
            var dataNationalityOptions = (masterNationality || []).map((a) => {
                return {
                    value: a.id,
                    labelTh: a.nameTh,
                    labelEn: a.nameEn,
                } as OptionMultiLanguageProps;
            });
            setNationalityOptions(dataNationalityOptions);
            setValue("tranSaleRefCode", localStorage.getItem("ref") || "");
            setValue("telephoneNumber", localStorage.getItem("ref") === "no-otp" ? "-" : (localStorage.getItem("check_phone") || ""));
            setValue("dateOfBirth", new Date(localStorage.getItem("check_dateOfBirth") || ""));
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
                    {
                        (watch().tranSaleRefCode !== "no-otp" || watch().tranSaleRefCode !== "walkin") && (
                            <div className="card mb-3">
                                <div className="card-body">
                                    <div className="row">
                                        <div className="col-sm-12">
                                            <InputFieldComponent
                                                name="tranSaleRefCode"
                                                label={t("registerDetail.form.tranSaleRefCode")}
                                                mode={InputMode.secondary}
                                                labelAlignClassName="text-left"
                                                labelClassName="col-sm-12"
                                                inputClassName="col-sm-12"
                                                renderControl={(field) => (
                                                    <>
                                                        <Input {...field} type={"hidden"} />
                                                        <Input type={"text"} value={refCodeName} disabled={true} />
                                                    </>
                                                )}
                                            ></InputFieldComponent>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )
                    }
                    <div className="card mb-3">
                        <CardHeader>
                            <CardTitle tag="h5">
                                {t("registerDetail.header.information")}
                            </CardTitle>
                        </CardHeader>
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="fullName"
                                        label={t("registerDetail.form.fullName")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                maxLength={256}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                {i18n.language === "th" ? (
                                    <Col sm={12}>
                                        <InputFieldComponent
                                            name="citizenIdentification"
                                            label={t("registerDetail.form.citizenIdentification")}
                                            mode={InputMode.secondary}
                                            labelAlignClassName="text-left"
                                            labelClassName="col-sm-12"
                                            inputClassName="col-sm-12"
                                            renderControl={(field) => (
                                                <Input
                                                    {...field}
                                                    maxLength={13}
                                                />
                                            )}
                                        ></InputFieldComponent>
                                    </Col>
                                ) : (
                                    <Col sm={12}>
                                        <InputFieldComponent
                                            name="passportNumber"
                                            label={t("registerDetail.form.passportNumber")}
                                            mode={InputMode.secondary}
                                            labelAlignClassName="text-left"
                                            labelClassName="col-sm-12"
                                            inputClassName="col-sm-12"
                                            renderControl={(field) => (
                                                <Input
                                                    {...field}
                                                    maxLength={20}
                                                />
                                            )}
                                        ></InputFieldComponent>
                                    </Col>
                                )}
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="address"
                                        label={t("registerDetail.form.address")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <>
                                                <Input
                                                    {...field}
                                                    as="textarea"
                                                    rows={5}
                                                    maxLength={maxLength}
                                                />
                                                <div style={{ marginTop: '8px', textAlign: 'right' }}>
                                                    {field.value.length}/{maxLength}
                                                </div>
                                            </>
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="dateOfBirth"
                                        label={t("registerDetail.form.dateOfBirth")}
                                        require={true}
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
                                                shouldDisableDate={(date: Date) => date > new Date()}
                                                oneTap
                                                disabled={true}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="telephoneNumber"
                                        label={t("registerDetail.form.telephoneNumber")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                type={"text"}
                                                maxLength={10}
                                                disabled={true}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="masterNationality"
                                        label={t("registerDetail.form.masterNationality")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <SelectPicker
                                                {...field}
                                                className="w-100"
                                                data={(nationalityOptions || []).map((a) => {
                                                    return {
                                                        value: a.value,
                                                        label:
                                                            i18n.language === "th"
                                                                ? a.labelTh
                                                                : i18n.language === "en"
                                                                    ? a.labelEn
                                                                    : a.labelTh,
                                                    } as OptionProps;
                                                })}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                            </Row>
                        </CardBody>
                    </div>
                    <div className="card mb-3">
                        <CardHeader>
                            <CardTitle tag="h5">{t("registerDetail.header.other")}</CardTitle>
                        </CardHeader>
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="lastMonthlyPeriod"
                                        label={t("registerDetail.form.lastMonthlyPeriod")}
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
                                                shouldDisableDate={(date: Date) => date > new Date()}
                                                oneTap
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="gestationalAge"
                                        label={t("registerDetail.form.gestationalAge")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                type={"number"}
                                                step={0.01}
                                                min={0}
                                                max={999}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="historyOfCesareanSection"
                                        label={t("registerDetail.form.historyOfCesareanSection")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <SelectPicker
                                                {...field}
                                                className="w-100"
                                                searchable={false}
                                                data={[
                                                    {
                                                        value: "no",
                                                        label: `${t(
                                                            "registerDetail.fixOption.historyOfCesareanSection.no"
                                                        )}`,
                                                    },
                                                    {
                                                        value: "yes",
                                                        label: `${t(
                                                            "registerDetail.fixOption.historyOfCesareanSection.yes"
                                                        )}`,
                                                    },
                                                ]}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="marriedOrBoyfriend"
                                        label={t("registerDetail.form.marriedOrBoyfriend")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <SelectPicker
                                                {...field}
                                                className="w-100"
                                                searchable={false}
                                                data={[
                                                    {
                                                        value: "no",
                                                        label: `${t(
                                                            "registerDetail.fixOption.marriedOrBoyfriend.no"
                                                        )}`,
                                                    },
                                                    {
                                                        value: "yes",
                                                        label: `${t(
                                                            "registerDetail.fixOption.marriedOrBoyfriend.yes"
                                                        )}`,
                                                    },
                                                ]}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="drugAllergy"
                                        label={t("registerDetail.form.drugAllergy")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                type={"text"}
                                                maxLength={64}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="congenitalDisease"
                                        label={t("registerDetail.form.congenitalDisease")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                type={"text"}
                                                maxLength={64}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="reasonTermination"
                                        label={t("registerDetail.form.reasonTermination")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                type={"text"}
                                                maxLength={64}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="informationToDoctor"
                                        label={t("registerDetail.form.informationToDoctor")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <>
                                                <Input
                                                    {...field}
                                                    as="textarea"
                                                    rows={5}
                                                    maxLength={maxLength}
                                                />
                                                <div style={{ marginTop: '8px', textAlign: 'right' }}>
                                                    {field.value.length}/{maxLength}
                                                </div>
                                            </>
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                            </Row>
                        </CardBody>
                    </div>
                    <div className="card mb-3">
                        <CardHeader>
                            <CardTitle tag="h5">
                                {t("registerDetail.header.service")}
                            </CardTitle>
                        </CardHeader>
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="receiveServiceDate"
                                        require={true}
                                        label={t("registerDetail.form.receiveServiceDate")}
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
                    </div>
                    <Row className="mb-3">
                        <Col xs={6}>
                            <Button
                                color="red"
                                appearance="primary"
                                className="w-100"
                                onClick={() => {
                                    onRemoveLocalStorage();
                                    navigate("/");
                                }}
                            >
                                {t("registerDetail.btn.btnCancel")}
                            </Button>
                        </Col>
                        <Col xs={6}>
                            <Button
                                color="green"
                                appearance="primary"
                                className="w-100"
                                onClick={async () => {
                                    const isValid = await trigger();
                                    if (isValid) {
                                        MySwal.fire({
                                            icon: 'question',
                                            html: `${t("registerDetail.alert.title")} ${getValues().receiveServiceDate &&
                                                moment.tz(getValues().receiveServiceDate, "Asia/Bangkok").format("DD/MM/YYYY")}`,
                                            showCancelButton: true,
                                            confirmButtonText: `${t("registerDetail.btn.btnConfirm")}`,
                                            cancelButtonText: `${t("registerDetail.btn.btnEdit")}`,
                                            confirmButtonColor: '#3085d6',
                                            cancelButtonColor: '#d33',
                                            reverseButtons: true,
                                            allowOutsideClick: false,
                                            allowEscapeKey: false,
                                        }).then(async (result) => {
                                            if (result.isConfirmed) {
                                                onSubmit(getValues());
                                            }
                                        });
                                    }
                                }}
                            >
                                {t("registerDetail.btn.btnRegister")}
                            </Button>
                        </Col>
                    </Row>
                </>
            )}
            <JsonViewerComponent data={watch()}></JsonViewerComponent>
            <JsonViewerComponent data={errors}></JsonViewerComponent>
        </FormProvider>
    );
};

export default RegisterDetail;
