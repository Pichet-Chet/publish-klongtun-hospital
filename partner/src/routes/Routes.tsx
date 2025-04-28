import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import NotAuthRoute from "./NotAuthRoute";
import ProtectedRoute from "./ProtectedRoute";
import Login from "../pages/login/Login";
import Home from "../pages/home/Home";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "login", element: (
                    <NotAuthRoute>
                        <Login />
                    </NotAuthRoute>
                )
            },
            {
                path: "",
                element: (
                    <ProtectedRoute>
                        <Home />
                    </ProtectedRoute>
                ),
            },
            {
                path: "home",
                element: (
                    <ProtectedRoute>
                        <Home />
                    </ProtectedRoute>
                ),
            },
            // {
            //     path: "case-list",
            //     element: (
            //         <ProtectedRoute>
            //             <CaseList />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "case-list/:id",
            //     element: (
            //         <ProtectedRoute>
            //             <CaseDetail />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "client-list",
            //     element: (
            //         <ProtectedRoute>
            //             <ClientList />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "client-list/:id",
            //     element: (
            //         <ProtectedRoute>
            //             <ClientDetail />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "waiting-us",
            //     element: (
            //         <ProtectedRoute>
            //             <WaitingUs />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "waiting-discount",
            //     element: (
            //         <ProtectedRoute>
            //             <WaitingDiscount />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "waiting-approve-case",
            //     element: (
            //         <ProtectedRoute>
            //             <WaitingApproveCase />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "finance-refund",
            //     element: (
            //         <ProtectedRoute>
            //             <FinanceRefund />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "finance-summary",
            //     element: (
            //         <ProtectedRoute>
            //             <FinanceSummary />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "finance-reserve",
            //     element: (
            //         <ProtectedRoute>
            //             <FinanceReserve />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "manage-staff",
            //     element: (
            //         <ProtectedRoute>
            //             <ManageStaff />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "manage-sale",
            //     element: (
            //         <ProtectedRoute>
            //             <ManageSale />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "manage-physician",
            //     element: (
            //         <ProtectedRoute>
            //             <ManagePhysician />
            //         </ProtectedRoute>
            //     ),
            // },
            // {
            //     path: "manage-item",
            //     element: (
            //         <ProtectedRoute>
            //             <ManageItem />
            //         </ProtectedRoute>
            //     ),
            // },
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