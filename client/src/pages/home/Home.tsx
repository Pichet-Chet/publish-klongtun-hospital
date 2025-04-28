import React, { useEffect } from "react";
import { useTranslation } from 'react-i18next';
import { useSearchParams, useNavigate } from "react-router-dom";
import { Button, FlexboxGrid, Panel } from 'rsuite';
import { useAuth } from "../../context/UseAuth";
import CustomFooter from "../../components/footer/Footer";

const Home = (props: any) => {
    const { t, i18n } = useTranslation("home");
    const { isLoggedIn } = useAuth();
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();

    useEffect(() => {
        var gettingData = true;
        if (gettingData) {
            var refCode = searchParams.get('ref') || "";
            if (refCode === "") {
                var refCode = localStorage.getItem("ref") || "";
            } else {
                localStorage.setItem("ref", refCode);
            }
        }
        return () => { gettingData = false };
    }, []);

    return (
        <>
            <div className="container-fluid d-flex flex-column p-0" style={{ minHeight: 'calc(100vh)' }}>
                <div className="p-3">
                    <div className="text-center mb-4 mt-5">
                        <img src={"./logo.png"} className="mb-3 img-fluid" />
                        <h5 className="card-title">{t("title")}</h5>
                    </div>

                    <div className="row d-flex justify-content-center" style={{ marginTop: "100px", marginBottom: "90px" }}>
                        <div className="col-md-6">
                            <div className="d-grid gap-3 mb-4">
                                <button
                                    className="btn btn-outline-success btn-lg"
                                    onClick={() => {
                                        if (isLoggedIn()) {
                                            navigate('/booking');
                                        } else {
                                            navigate('/register');
                                        }
                                    }}>
                                    {t("btn.btnLogin")}
                                </button>
                                <button
                                    className="btn btn-outline-success btn-lg"
                                    onClick={() => {
                                        if (isLoggedIn()) {
                                            navigate('/changeappointment');
                                        } else {
                                            navigate('/verification');
                                        }
                                    }}>
                                    {t("btn.btnVerification")}
                                </button>
                            </div>
                        </div>
                    </div>

                    <div className="text-center">
                        <p className="text-muted mb-4">
                            {t("footer")}
                        </p>

                        <div className="d-flex justify-content-center gap-3">
                            <img
                                role="button"
                                src={"./thailand.png"}
                                alt="Thai"
                                style={{ width: '30px' }}
                                onClick={() => {
                                    localStorage.setItem("language", "th");
                                    i18n.changeLanguage("th");
                                }}
                            />
                            <img
                                role="button"
                                src={"./united-kingdom.png"}
                                alt="English"
                                style={{ width: '30px' }}
                                onClick={() => {
                                    localStorage.setItem("language", "en");
                                    i18n.changeLanguage("en");
                                }}
                            />
                        </div>
                    </div>
                </div>
                <CustomFooter />
            </div>
        </>
    );
};

export default Home;