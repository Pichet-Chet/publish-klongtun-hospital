import React from "react";
import { Helmet } from "react-helmet"
import { Outlet } from "react-router";
import "react-datepicker/dist/react-datepicker.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import "react-toastify/dist/ReactToastify.css";
import "./App.css";
import { ToastContainer } from "react-toastify";
import { HelperProvider } from "./context/Helper";
import { UserProvider } from "./context/UseAuth";
import AppContent from "./components/containers/Content";

function App() {
  return (
    <>
      <Helmet>
        <title>{"Partners Klongtun Hospital"}</title>
      </Helmet>
      <HelperProvider>
        <UserProvider>
          <AppContent>
            <Outlet />
            <ToastContainer />
          </AppContent>
        </UserProvider>
      </HelperProvider>
    </>
  );
}

export default App;