import React from 'react';
import {
    Navbar,
    NavbarBrand,
    NavbarText,
} from 'reactstrap';
import { useTranslation } from 'react-i18next';

const CustomNavbar = (props: any) => {
    const { t, i18n } = useTranslation("home");
    return (
        <div>
            <Navbar
                color='light'
                expand={true}
                container={'fluid'}
                fixed='top'
                className='shadow-sm'
                {...props}>
                <NavbarBrand>
                    <img src={"./logo.png"} style={{ width: '170px' }} />
                </NavbarBrand>
                <NavbarText className="d-flex justify-content-center gap-2">
                    <img
                        role="button"
                        src={"./thailand.png"}
                        alt="Thai"
                        style={{ width: '30px' }}
                        onClick={() => {
                            localStorage.setItem("language", "th");
                            i18n.changeLanguage("th");
                        }}
                    />
                    <img
                        role="button"
                        src={"./united-kingdom.png"}
                        alt="English"
                        style={{ width: '30px' }}
                        onClick={() => {
                            localStorage.setItem("language", "en");
                            i18n.changeLanguage("en");
                        }}
                    />
                </NavbarText>
            </Navbar>
        </div>
    );
};

export default CustomNavbar;
