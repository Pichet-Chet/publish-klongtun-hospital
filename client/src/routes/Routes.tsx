import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import NotAuthRoute from "./NotAuthRoute";
import ProtectedRoute from "./ProtectedRoute";
import Register from "../pages/register/Register";
import Verification from "../pages/verification/Verification";
// import Login from "../pages/login/Login";
import Home from "../pages/home/Home";
import Booking from "../pages/booking/Booking";
import ChangeAppointment from "../pages/changeappointment/ChangeAppointment";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <Home /> },
            // {
            //     path: "login", element: (
            //         <NotAuthRoute>
            //             <Login />
            //         </NotAuthRoute>
            //     )
            // },
            {
                path: "register", element: (
                    <NotAuthRoute>
                        <Register />
                    </NotAuthRoute>
                )
            },
            {
                path: "verification", element: (
                    <NotAuthRoute>
                        <Verification />
                    </NotAuthRoute>
                )
            },
            {
                path: "booking",
                element: (
                    <ProtectedRoute>
                        <Booking />
                    </ProtectedRoute>
                ),
            },
            {
                path: "changeappointment",
                element: (
                    <ProtectedRoute>
                        <ChangeAppointment />
                    </ProtectedRoute>
                ),
            },
            // {
            //     path: "company/:ticker",
            //     element: (
            //         <ProtectedRoute>
            //             <CompanyPage />
            //         </ProtectedRoute>
            //     ),
            //     children: [
            //         { path: "company-profile", element: <CompanyProfile /> },
            //         { path: "income-statement", element: <IncomeStatement /> },
            //         { path: "balance-sheet", element: <BalanceSheet /> },
            //         { path: "cashflow-statement", element: <CashflowStatement /> },
            //         { path: "historical-dividend", element: <HistoricalDividend /> },
            //     ],
            // },
        ],
    },
]);