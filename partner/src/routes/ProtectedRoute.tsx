import React, { useEffect } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { toast } from "react-toastify";
import { useAuth } from "../context/UseAuth";
import { AuthProfileProps } from "../_types/Client";
import { useClientState } from "../_store/Client";
import { useHelper } from "../context/Helper";

type Props = { children: React.ReactNode };

const ProtectedRoute = ({ children }: Props) => {
    const location = useLocation();
    const { isLoggedIn, profile, logout } = useAuth();
    const { getMessage } = useHelper();
    const getProfile = useClientState((state) => state.getProfile);

    const checkProfile = async (uuid?: string) => {
        await getProfile(uuid).then((res: ResponseProps) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                } else {
                    toast.error(getMessage(res));
                    logout();
                }
            } else {
                toast.error(getMessage(res));
                logout();
            }
        });
    }

    useEffect(() => {
        var _profile: AuthProfileProps = profile();
        if (_profile?.id) {
            checkProfile(_profile?.id);
        } else {
            // toast.error("ออกจากระบบ");
        }
    }, [])

    return isLoggedIn() ? (
        <>
            {children}
        </>
    ) : (
        <Navigate to="/login" state={{ from: location }} replace />
    );
};

export default ProtectedRoute;