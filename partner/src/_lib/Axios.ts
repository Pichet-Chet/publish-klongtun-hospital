import axios from "axios";

export const API_APP_URL = process.env.REACT_APP_API_URL;

const instance = axios.create();
instance.interceptors.request.use(async (request) => {
  const access_token = localStorage.getItem("token");
  request.headers.set("Authorization", `Bearer ${access_token ?? ""}`);
  request.headers.set("Access-Control-Allow-Origin", "*");
  request.headers.set(
    "Access-Control-Allow-Methods",
    "GET, POST, PUT, DELETE, OPTIONS, FETCH"
  );
  request.headers.set(
    "Access-Control-Allow-Headers",
    "Content-Type, Authorization"
  );
  return request;
});

instance.interceptors.response.use(
  async (response) => {
    return response;
  },
  async (error) => {
    return Promise.reject(error);
  }
);

export default instance;
