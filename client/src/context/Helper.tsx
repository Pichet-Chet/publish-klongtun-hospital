import React, { createContext, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

type HelperType = {
  getMessage: (res: ResponseProps) => string;
};

type Props = { children: React.ReactNode };

const HelperContext = createContext<HelperType>({} as HelperType);

export const HelperProvider = ({ children }: Props) => {
  const [isReady, setIsReady] = useState(false);
  const { i18n } = useTranslation("home");

  useEffect(() => {
    setIsReady(true);
  }, []);

  const getMessage = (res: ResponseProps) => {
    var alertMsg = i18n.language === "th"
      ? res.data?.output.messageAlert.th
      : i18n.language === "en"
        ? res.data?.output.messageAlert.en
        : res.data?.output.messageAlert.th;
    return alertMsg || "";
  };

  return (
    <HelperContext.Provider
      value={{ getMessage }}
    >
      {isReady ? children : null}
    </HelperContext.Provider>
  );
};

export const useHelper = () => React.useContext(HelperContext);
