import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ChangeAppointmentDetail from "./ChangeAppointmentDetail";
import { useAuth } from "../../context/UseAuth";
import CustomNavbar from "../../components/navbar/Navbar";
import { useChangeAppointmentState } from "../../_store/ChangeAppointment";
import { useClientState } from "../../_store/Client";
import { UserHaveCaesProps } from "../../_types/Client";
import Spinner from "../../components/spinners/Spinner";
import { useHelper } from "../../context/Helper";
import { Message, useToaster } from 'rsuite';
import CustomFooter from "../../components/footer/Footer";

const messageError = (msg: String) => (
    <Message showIcon type={"error"} closable>
        <strong>แจ้งเตือน!</strong> {msg}
    </Message>
);
const ChangeAppointment = (props: any) => {
    const navigate = useNavigate();
    const toaster = useToaster();
    const { getMessage } = useHelper();
    const { user } = useAuth();
    const isLoading = useChangeAppointmentState((state) => state.isLoading);
    const setIsLoading = useChangeAppointmentState((state) => state.setIsLoading);
    const getCaesRg01 = useClientState((state) => state.getCaesRg01);
    const [userHaveCaes, setUserHaveCaes] = useState<UserHaveCaesProps>({});


    useEffect(() => {
        setIsLoading(true);
        var gettingData = true;
        if (gettingData) {
            getCaesRg01(user?.id).then((res) => {
                if (res.data?.code === 200) {
                    if (res.data?.status === true) {
                        var dataCase = res.data.output.data as UserHaveCaesProps;
                        if (!dataCase.caseIsActive) {
                            navigate("/booking");
                        } else {
                            setUserHaveCaes(dataCase);
                            setIsLoading(false);
                        }
                    } else {
                        setIsLoading(false);
                        toaster.push(messageError(getMessage(res)), { duration: 5000 });
                    }
                } else {
                    setIsLoading(false);
                    toaster.push(messageError(getMessage(res)), { duration: 5000 });
                }
            });
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
                                    {
                                        <ChangeAppointmentDetail transCase={userHaveCaes}></ChangeAppointmentDetail>
                                    }
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

export default ChangeAppointment;
