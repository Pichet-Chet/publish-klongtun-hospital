import i18next from "i18next";

import home_th from "./th/home.json";
import home_en from "./en/home.json";
import login_th from "./th/login.json";
import login_en from "./en/login.json";

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
      home: home_th,
      login: login_th,
    },
    en: {
      home: home_en,
      login: login_en,
    },
  },
});

export default i18next;