import React, { useState, useEffect } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import CustomNavbar from "../../components/navbar/Navbar";
import RegisterDetail from "./RegisterDetail";
import RegisterCheckData from "./RegisterCheckData";
import Spinner from "../../components/spinners/Spinner";
import { useRegisterState } from "../../_store/Register";
import { useMasterState } from "../../_store/Master";
import { MasterNationalityProps } from "../../_types/Master";
import CustomFooter from "../../components/footer/Footer";

const MySwal = withReactContent(Swal);
const Register = (props: any) => {
    const { t } = useTranslation("register");
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const isLoading = useRegisterState((state) => state.isLoading);
    const setIsLoading = useRegisterState((state) => state.setIsLoading);
    const getNameByRefCode = useRegisterState((state) => state.getNameByRefCode);
    const getMasterNationality = useMasterState((state) => state.getMasterNationality);
    const setMasterNationality = useMasterState((state) => state.setMasterNationality);
    const [isRegister, setIsRegister] = useState<boolean>(false);
    const [refCodeName, setRefCodeName] = useState("");

    const isSetRegister = (isRegister: boolean) => {
        setIsRegister(isRegister);
    }

    useEffect(() => {
        setIsLoading(true);
        var gettingData = true;
        if (gettingData) {
            var refCode = searchParams.get('ref') || "";
            if (refCode === "") {
                var refCode = localStorage.getItem("ref") || "";
            } else {
                localStorage.setItem("ref", refCode);
            }

            if (refCode !== "") {
                const getAllMaster = async () => {
                    await getMasterNationality().then((res) => {
                        var dataMasterCountry = res.data?.output.data as MasterNationalityProps[];
                        setMasterNationality(dataMasterCountry);
                    });
                    await getNameByRefCode(localStorage.getItem("ref") || "").then((response) => {
                        if (response.data?.code === 200) {
                            if (response.data?.status === true) {
                                const saleName = response.data.output.data as any;
                                setRefCodeName(saleName?.fullName);
                            }
                        }
                    });
                };
                getAllMaster().then(() => {
                    setIsLoading(false);
                    if (refCode === "no-otp") {
                        isSetRegister(true);
                    }
                });
            } else {
                MySwal.fire({
                    icon: 'error',
                    text: `${t("registerOtp.alert.noRef")}`,
                    confirmButtonText: `${t("registerOtp.btn.btnOk")}`,
                    confirmButtonColor: '#3085d6',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then((result) => {
                    navigate("/");
                });
            }
        }
        return () => {
            gettingData = false;
        };
    }, []);

    return (
        <>
            {isLoading ? (
                <Spinner />
            ) : (
                <>
                    <CustomNavbar />
                    <div className="container-fluid d-flex flex-column p-0" style={{ minHeight: 'calc(100vh)', backgroundColor: "#e3e3e3" }}>
                        <div className="p-3">
                            <div className="row d-flex justify-content-center" style={{ marginTop: "100px", marginBottom: "90px" }}>
                                <div className="col-md-6 mb-4">
                                    <h6 className="card-title mb-2" style={{ fontSize: "18px" }}>
                                        {t("title")}
                                    </h6>
                                    <p className="card-title mb-2" style={{ fontSize: "14px" }}>
                                        {isRegister === true ? (
                                            <>{t("titleDetail")}</>
                                        ) : (
                                            <>{t("titleDetailOTP")}</>
                                        )}
                                    </p>
                                    {isRegister === true ? (
                                        <RegisterDetail refCodeName={refCodeName}></RegisterDetail>
                                    ) : (
                                        <RegisterCheckData isSetRegister={isSetRegister}></RegisterCheckData>
                                    )}
                                </div>
                            </div>
                        </div>
                        <CustomFooter />
                    </div>
                </>
            )}
        </>
    );
};

export default Register;
