import React from 'react';
import { useTranslation } from 'react-i18next';

const CustomFooter = (props: any) => {
    const { t } = useTranslation("footer");
    return (
        <footer className="py-3 mt-auto" style={{ backgroundColor: '#01904b' }}>
            <div className="container text-center">
                <h5 className="text-white mt-3" style={{ fontSize: "16px" }}>
                    {t("title")}
                </h5>
                <p className="text-white m-0 mb-1" style={{ fontSize: "0.9rem" }}>
                    {t("detail")}{' '}{t("phone")}
                </p>
            </div>
        </footer>
    );
};

export default CustomFooter;
