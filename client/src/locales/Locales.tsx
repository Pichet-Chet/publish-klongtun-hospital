import i18next from "i18next";
import global_th from "./th/global.json";
import global_en from "./en/global.json";
import home_th from "./th/home.json";
import home_en from "./en/home.json";
import login_th from "./th/login.json";
import login_en from "./en/login.json";
import register_th from "./th/register.json";
import register_en from "./en/register.json";
import booking_th from "./th/booking.json";
import booking_en from "./en/booking.json";
import changeappointment_th from "./th/changeappointment.json";
import changeappointment_en from "./en/changeappointment.json";
import error_th from "./th/error.json";
import error_en from "./en/error.json";
import footer_th from "./th/footer.json";
import footer_en from "./en/footer.json";

if (!localStorage.getItem("language")) {
  localStorage.setItem("language", "th");
}
const language = localStorage.getItem("language") || 'th';
i18next.init({
  interpolation: { escapeValue: false },
  lng: language,
  fallbackLng: language,
  resources: {
    th: {
      global: global_th,
      home: home_th,
      login: login_th,
      register: register_th,
      booking: booking_th,
      changeappointment: changeappointment_th,
      error: error_th,
      footer: footer_th,
    },
    en: {
      global: global_en,
      home: home_en,
      login: login_en,
      register: register_en,
      booking: booking_en,
      changeappointment: changeappointment_en,
      error: error_en,
      footer: footer_en,
    },
  },
});

export default i18next;