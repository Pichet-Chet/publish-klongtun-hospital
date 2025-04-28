import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useBookingState } from "../../_store/Booking";
import { BookingUserRequestFormProps, BookingUserRequestApiProps } from "../../_types/Booking";
import { TransClientProps } from "../../_types/Client";
import { useMasterState } from "../../_store/Master";
import { useAuth } from "../../context/UseAuth";
import { useHelper } from "../../context/Helper";
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
const BookingDetail = ({ client }: { client: TransClientProps }) => {
    const { t, i18n } = useTranslation("booking");
    const toaster = useToaster();
    const navigate = useNavigate();
    const { user } = useAuth();
    const { getMessage } = useHelper();
    const isLoading = useBookingState((state) => state.isLoading);
    const setIsLoading = useBookingState((state) => state.setIsLoading);
    const validationRequestBookingData = useBookingState(
        (state) => state.validationRequestBookingData
    );
    const requestBooking = useBookingState((state) => state.requestBooking);
    const updateClient = useBookingState((state) => state.updateClient);
    const masterNationality = useMasterState((state) => state.masterNationality);
    const [nationalityOptions, setNationalityOptions] = useState<OptionMultiLanguageProps[]>([]);
    const maxLength = 256;

    const methods = useForm<BookingUserRequestFormProps>({
        mode: "onChange",
        resolver: yupResolver(validationRequestBookingData(t)),
        defaultValues: undefined,
    });
    const { formState: { errors }, getValues, watch, trigger, reset } = methods;

    const onSubmit: SubmitHandler<BookingUserRequestFormProps> = async (data) => {
        setIsLoading(true);
        var postDataClient: TransClientProps = {
            id: client.id,
            fullName: data.fullName,
            citizenIdentification: data.citizenIdentification,
            passportNumber: client.passportNumber,
            address: data.address,
            dateOfBirth: data.dateOfBirth ? moment
                .tz(data.dateOfBirth, "Asia/Bangkok")
                .format("YYYY-MM-DD")
                : undefined,
            telephoneNumber: data.telephoneNumber,
            masterNationalityId: data.masterNationalityId,
            isActive: client.isActive,
            updatedBy: client.updatedBy,
            telephoneCode: client.telephoneCode,
            telephoneSecond: client.telephoneCode,
            masterRightTreatmentId: client.masterRightTreatmentId,
            tranSaleRefCode: client.tranSaleRefCode,
            hnNo: client.hnNo,
            occupation: client.occupation,
            hostpitalName: client.hostpitalName
        };
        await updateClient(postDataClient).then(async (response: ResponseProps) => {
            if (response.data?.code === 200) {
                if (response.data?.status === true) {
                    var postData: BookingUserRequestApiProps = {
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
                        transClientId: user?.id,
                        isActive: true,
                        createdBy: user?.id,
                        masterStatusCode: "RG-01",
                        masterConsultRoomId: ""
                    };
                    await requestBooking(postData).then((res: ResponseProps) => {
                        if (res.data?.code === 200) {
                            if (res.data?.status === true) {
                                MySwal.fire({
                                    icon: 'success',
                                    text: `${t("bookingDetail.alert.submitSuccess")}`,
                                    confirmButtonText: `${t("bookingDetail.btn.btnOk")}`,
                                    confirmButtonColor: '#3085d6',
                                    allowOutsideClick: false,
                                    allowEscapeKey: false,
                                }).then(async (result) => {
                                    navigate("/");
                                });
                            } else {
                                toaster.push(messageError(getMessage(res)), { duration: 5000 });
                            }
                        } else {
                            toaster.push(messageError(getMessage(res)), { duration: 5000 });
                        }
                    });
                } else {
                    toaster.push(messageError(getMessage(response)), { duration: 5000 });
                }
            } else {
                toaster.push(messageError(getMessage(response)), { duration: 5000 });
            }
        });
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
            reset({
                fullName: client.fullName || "",
                citizenIdentification: client.citizenIdentification || "",
                address: client.address || "",
                dateOfBirth: client.dateOfBirth ? new Date(client.dateOfBirth) : null,
                telephoneNumber: client.telephoneNumber || "",
                masterNationalityId: client.masterNationalityId || "",
            });
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
                    <Card className="mt-2 mb-3">
                        <CardHeader>
                            <CardTitle tag="h5">{t("bookingDetail.header.information")}</CardTitle>
                        </CardHeader>
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="fullName"
                                        label={t("bookingDetail.form.fullName")}
                                        require={true}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                maxLength={256}
                                                disabled={client.fullName !== null}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="citizenIdentification"
                                        label={t("bookingDetail.form.citizenIdentification")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                maxLength={13}
                                                disabled={client.citizenIdentification !== null}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="address"
                                        label={t("bookingDetail.form.address")}
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
                                                    disabled={client.address !== null}
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
                                        label={t("bookingDetail.form.dateOfBirth")}
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
                                                disabled={client.dateOfBirth !== null}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="telephoneNumber"
                                        label={t("bookingDetail.form.telephoneNumber")}
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
                                                disabled={client.telephoneNumber !== null}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="masterNationality"
                                        label={t("bookingDetail.form.masterNationality")}
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
                                                disabled={client.masterNationalityId !== null}
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                    <Card className="mt-3 mb-3">
                        <CardHeader>
                            <CardTitle tag="h5">{t("bookingDetail.header.other")}</CardTitle>
                        </CardHeader>
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="lastMonthlyPeriod"
                                        label={t("bookingDetail.form.lastMonthlyPeriod")}
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
                                        label={t("bookingDetail.form.gestationalAge")}
                                        mode={InputMode.secondary}
                                        labelAlignClassName="text-left"
                                        labelClassName="col-sm-12"
                                        inputClassName="col-sm-12"
                                        renderControl={(field) => (
                                            <Input
                                                {...field}
                                                type={"number"}
                                                step={1}
                                                min={1}
                                                max={999}
                                                autoComplete="off"
                                            />
                                        )}
                                    ></InputFieldComponent>
                                </Col>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="historyOfCesareanSection"
                                        label={t("bookingDetail.form.historyOfCesareanSection")}
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
                                                            "bookingDetail.fixOption.historyOfCesareanSection.no"
                                                        )}`,
                                                    },
                                                    {
                                                        value: "yes",
                                                        label: `${t(
                                                            "bookingDetail.fixOption.historyOfCesareanSection.yes"
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
                                        label={t("bookingDetail.form.marriedOrBoyfriend")}
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
                                                            "bookingDetail.fixOption.marriedOrBoyfriend.no"
                                                        )}`,
                                                    },
                                                    {
                                                        value: "yes",
                                                        label: `${t(
                                                            "bookingDetail.fixOption.marriedOrBoyfriend.yes"
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
                                        label={t("bookingDetail.form.drugAllergy")}
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
                                        label={t("bookingDetail.form.congenitalDisease")}
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
                                        label={t("bookingDetail.form.reasonTermination")}
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
                                        label={t("bookingDetail.form.informationToDoctor")}
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
                    </Card>
                    <Card className="mb-3">
                        <CardHeader>
                            <CardTitle tag="h5">
                                {t("bookingDetail.header.service")}
                            </CardTitle>
                        </CardHeader>
                        <CardBody>
                            <Row>
                                <Col sm={12}>
                                    <InputFieldComponent
                                        name="receiveServiceDate"
                                        require={true}
                                        label={t("bookingDetail.form.receiveServiceDate")}
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
                        <Col xs={6}>
                            <Button
                                color="red"
                                appearance="primary"
                                className="w-100"
                                onClick={() => {
                                    navigate("/");
                                }}
                            >
                                {t("bookingDetail.btn.btnCancel")}
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
                                            html: `${t("bookingDetail.alert.title")} ${getValues().receiveServiceDate &&
                                                moment.tz(getValues().receiveServiceDate, "Asia/Bangkok").format("DD/MM/YYYY")}`,
                                            showCancelButton: true,
                                            confirmButtonText: `${t("bookingDetail.btn.btnOk")}`,
                                            cancelButtonText: `${t("bookingDetail.btn.btnEdit")}`,
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
                                {t("bookingDetail.btn.btnSubmit")}
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

export default BookingDetail;
