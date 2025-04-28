import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import BookingDetail from "./BookingDetail";
import { useAuth } from "../../context/UseAuth";
import CustomNavbar from "../../components/navbar/Navbar";
import { useBookingState } from "../../_store/Booking";
import { useClientState } from "../../_store/Client";
import { UserHaveCaesProps, TransClientProps } from "../../_types/Client";
import { useMasterState } from "../../_store/Master";
import { MasterNationalityProps } from "../../_types/Master";
import Spinner from "../../components/spinners/Spinner";
import { useHelper } from "../../context/Helper";
import { Message, useToaster, Button } from 'rsuite';
import CustomFooter from "../../components/footer/Footer";

const messageError = (msg: String) => (
    <Message showIcon type={"error"} closable>
        <strong>แจ้งเตือน!</strong> {msg}
    </Message>
);
const Booking = (props: any) => {
    const { t } = useTranslation("booking");
    const { t: tg } = useTranslation("global");
    const toaster = useToaster();
    const navigate = useNavigate();
    const { getMessage } = useHelper();
    const { user, logout } = useAuth();
    const isLoading = useBookingState((state) => state.isLoading);
    const setIsLoading = useBookingState((state) => state.setIsLoading);
    const getCaesRg01 = useClientState((state) => state.getCaesRg01);
    const getProfile = useClientState((state) => state.getProfile);
    const getMasterNationality = useMasterState((state) => state.getMasterNationality);
    const setMasterNationality = useMasterState((state) => state.setMasterNationality);
    const [transClient, setTransClient] = useState<TransClientProps>({});

    const onLoadData = async () => {
        await getMasterNationality().then((res) => {
            var dataMasterCountry = res.data?.output.data as MasterNationalityProps[];
            setMasterNationality(dataMasterCountry);
        });
        await getProfile(user?.id).then((responseClient) => {
            if (responseClient.data?.code === 200) {
                if (responseClient.data?.status === true) {
                    var data = responseClient.data.output.data as TransClientProps;
                    setTransClient(data);
                } else {
                    toaster.push(messageError(getMessage(responseClient)), { duration: 5000 });
                }
            } else {
                toaster.push(messageError(getMessage(responseClient)), { duration: 5000 });
            }
        });
        await getCaesRg01(user?.id).then((res) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    var dataCase = res.data.output.data as UserHaveCaesProps;
                    if (dataCase.caseIsActive) {
                        navigate("/changeappointment");
                    }
                } else {
                    toaster.push(messageError(getMessage(res)), { duration: 5000 });
                }
            } else {
                toaster.push(messageError(getMessage(res)), { duration: 5000 });
            }
        });
    };
    useEffect(() => {
        setIsLoading(true);
        var gettingData = true;
        if (gettingData) {
            onLoadData().then((response) => {
                setIsLoading(false);
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
                                <div className="col-md-6 mb-2">
                                    <div className="row">
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
                                    {
                                        <BookingDetail client={transClient}></BookingDetail>
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

export default Booking;
