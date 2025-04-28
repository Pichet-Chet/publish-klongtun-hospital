import React, { useState } from 'react';
import {
    Navbar,
    NavbarBrand,
    NavbarText,
    Nav,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem
} from 'reactstrap';
import { useAuth } from "../../context/UseAuth";
import { BsPersonFill } from "react-icons/bs";


const CustomNavbar = (props: any) => {
    const { logout, profile } = useAuth();
    return (
        <div>
            <Navbar
                color='white'
                expand={true}
                container={'fluid'}
                fixed='top'
                className='shadow-sm'
                {...props}>
                <NavbarBrand>
                    <a href="/" style={{ textDecoration: 'none' }}>
                        <img src="/logo.png" alt="Logo" style={{ width: '170px' }} />
                    </a>
                </NavbarBrand>
                <NavbarText className="d-flex justify-content-center gap-2">
                    <Nav className="ml-auto" navbar>
                        <UncontrolledDropdown nav inNavbar className="logon-user ">
                            <DropdownToggle nav caret >
                                <BsPersonFill />
                                {profile().fullName}
                            </DropdownToggle>
                            <DropdownMenu end className="shadow-sm border-0">
                                <DropdownItem onClick={logout}>
                                    <a
                                        href={process.env.REACT_APP_HISTORY_BASENAME ? `${process.env.REACT_APP_HISTORY_BASENAME}/` : `/`}
                                        className="nav-link"
                                    >
                                        <i className="fas fa-arrow-right"></i> &nbsp; ออกจากระบบ
                                    </a>
                                </DropdownItem>
                            </DropdownMenu>
                        </UncontrolledDropdown>
                    </Nav>
                </NavbarText>
            </Navbar>
        </div>
    );
};

export default CustomNavbar;
