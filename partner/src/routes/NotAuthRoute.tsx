import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../context/UseAuth";

type Props = { children: React.ReactNode };

const NotAuthRoute = ({ children }: Props) => {
    const location = useLocation();
    const { isLoggedIn } = useAuth();
    return !isLoggedIn() ? (
        <>{children}</>
    ) : (
        <Navigate to="/home" state={{ from: location }} replace />
    );
};

export default NotAuthRoute;