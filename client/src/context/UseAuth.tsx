import React, { createContext, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { jwtDecode, JwtPayload } from "jwt-decode";
import { useClientState } from "../_store/Client";
import { LoginUserTokenProps } from "../_types/Login";
import { AuthProfileProps } from "../_types/Client";
import { useHelper } from "./Helper";
import { Message, useToaster } from 'rsuite';

interface CustomJwtPayload extends JwtPayload {
  sid?: string;
  roles?: string;
}

type UserContextType = {
  user: AuthProfileProps | null;
  token: string | null;
  loginUser: (response: ResponseProps) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);
const messageError = (msg: String) => (
  <Message showIcon type={"error"} closable>
    <strong>แจ้งเตือน!</strong> {msg}
  </Message>
);
const messageSuccess = (msg: String) => (
  <Message showIcon type={"success"} closable>
    <strong>แจ้งเตือน!</strong> {msg}
  </Message>
);
export const UserProvider = ({ children }: Props) => {
  const { t: tg } = useTranslation("global");
  const toaster = useToaster();
  const navigate = useNavigate();
  const { getMessage } = useHelper();
  const getProfile = useClientState((state) => state.getProfile);
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<AuthProfileProps | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const profile = localStorage.getItem("profile");
    const token = localStorage.getItem("token");
    if (profile && token) {
      setUser(JSON.parse(profile));
      setToken(token);
    }
    setIsReady(true);
  }, []);

  const loginUser = async (response: ResponseProps) => {
    var login = response.data?.output.data as LoginUserTokenProps;
    if (login.accessToken) {
      localStorage.setItem("token", login.accessToken);
      const decoded: CustomJwtPayload = jwtDecode<CustomJwtPayload>(login.accessToken);
      setToken(login.accessToken || "");
      if (decoded && decoded.sid) {
        await getProfile(decoded.sid).then((res: ResponseProps) => {
          if (res.data?.code === 200) {
            if (res.data?.status === true) {
              var outputProfile = res.data.output.data as AuthProfileProps;
              var profile = {
                id: outputProfile.id,
                fullName: outputProfile.fullName,
                telephoneCode: outputProfile.telephoneCode,
                telephoneNumber: outputProfile.telephoneNumber,
                tranSaleRefCode: outputProfile.tranSaleRefCode,
                dateOfBirth: outputProfile.dateOfBirth,
              } as AuthProfileProps;
              localStorage.setItem("profile", JSON.stringify(profile));
              setUser(profile!);
            } else {
              toaster.push(messageError(getMessage(res)), { duration: 5000 });
            }
          } else {
            toaster.push(messageError(getMessage(res)), { duration: 5000 });
          }
        });
      }
    }
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logout = async () => {
    localStorage.removeItem("token");
    localStorage.removeItem("profile");
    localStorage.removeItem("token");
    setUser(null);
    setToken("");
    navigate("/");
    toaster.push(messageSuccess(tg("logoutMessage")), { duration: 5000 });
  };

  return (
    <UserContext.Provider
      value={{ loginUser, user, token, logout, isLoggedIn }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => React.useContext(UserContext);
