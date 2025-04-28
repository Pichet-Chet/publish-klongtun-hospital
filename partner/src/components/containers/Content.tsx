import { Fragment, useEffect } from "react";
import _ from 'lodash';
import { useAuth } from "../../context/UseAuth";
import CustomNavbar from "../../components/navbar/Navbar";
import CustomSidebar from "../../components/sidebar/Sidebar";

type Props = { children: React.ReactNode };

const AppContent = ({ children }: Props) => {
    const { isLoggedIn } = useAuth();

    return (
        <Fragment>
            {
                !isLoggedIn() ?
                    <div className="container-fluid">
                        <div className="row">
                            <div className="col-12">
                                {children}
                            </div>
                        </div>
                    </div>
                    :
                    <>
                        <CustomNavbar />
                        <div className="container-fluid">
                            <div className="row flex-nowrap" style={{ minHeight: 'calc(100vh - 70px)', marginTop: '70px' }}>
                                {/* <CustomSidebar /> */}
                                <div className="col py-3 bg-light">
                                    {children}
                                </div>
                            </div>
                        </div>
                    </>
            }
        </Fragment>
    );
};

export default AppContent;