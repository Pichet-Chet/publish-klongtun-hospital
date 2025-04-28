import React, { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode, JwtPayload } from "jwt-decode";
import { toast } from "react-toastify";
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import { useClientState } from "../_store/Client";
import { LoginUserTokenProps } from "../_types/Login";
import { AuthProfileProps, AuthProfileApiProps, SysPermissionProps } from "../_types/Client";
import { useHelper } from "./Helper";

interface CustomJwtPayload extends JwtPayload {
  sid?: string;
  roles?: string;
}

type UserContextType = {
  token: string | null;
  profile: () => AuthProfileProps;
  loginUser: (response: ResponseProps) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
  statusCodeViewList: () => MasterStatusCode[];
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);
const MySwal = withReactContent(Swal);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const { getMessage } = useHelper();
  const getProfile = useClientState((state) => state.getProfile);
  const getPermission = useClientState((state) => state.getPermission);
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
      if (decoded.roles === "sale") {
        setToken(login.accessToken || "");
        if (decoded && decoded.sid) {
          await getProfile(decoded.sid).then(async (res: ResponseProps) => {
            if (res.data?.code === 200) {
              if (res.data?.status === true) {
                var outputProfile = res.data.output.data as AuthProfileApiProps;
                await getPermission(outputProfile.sysRoleId).then((resPermission: ResponseProps) => {
                  if (resPermission.data?.code === 200) {
                    if (resPermission.data?.status === true) {
                      var permissions = resPermission.data.output.data as SysPermissionProps[];
                      var profile = {
                        id: outputProfile.id,
                        fullName: outputProfile.fullName,
                        role: decoded.roles,
                        permission: permissions.map(a => a.page),
                        masterSaleGroupId: outputProfile.masterSaleGroupId
                      } as AuthProfileProps;
                      localStorage.setItem("profile", JSON.stringify(profile));
                      setUser(profile!);
                      navigate('/home');
                    } else {
                      toast.error(getMessage(resPermission));
                    }
                  } else {
                    toast.error(getMessage(resPermission));
                  }
                });
              } else {
                toast.error(getMessage(res));
              }
            } else {
              toast.error(getMessage(res));
            }
          });
        }
      } else {
        toast.error("คุณไม่มีสิทธิ์ใช้งานระบบนี้");
      }
    }
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const profile = () => {
    return user!;
  };

  const statusCodeViewList = () => {
    var statusCodes: MasterStatusCode[] = [];
    if (user?.role === "sale") {
    }
    return statusCodes;
  }

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("profile");
    setUser(null);
    setToken("");
    navigate("/login");
  };

  return (
    <UserContext.Provider
      value={{ loginUser, profile, token, logout, isLoggedIn, statusCodeViewList }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => React.useContext(UserContext);
