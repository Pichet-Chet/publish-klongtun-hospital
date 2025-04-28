import React from "react";
import { Helmet } from "react-helmet"
import { Outlet } from "react-router";
import "rsuite/dist/rsuite.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import { useTranslation } from 'react-i18next';
import "./App.css";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./context/UseAuth";
import { HelperProvider } from "./context/Helper";

function App() {
  const { t } = useTranslation("global");
  return (
    <>
      <Helmet>
        <title>{t("titleWeb")}</title>
      </Helmet>
      <HelperProvider>
        <UserProvider>
          <Outlet />
          <ToastContainer />
        </UserProvider>
      </HelperProvider>
    </>
  );
}

export default App;