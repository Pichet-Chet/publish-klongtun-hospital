import React, { useState } from 'react';
import { Sidebar, Menu, MenuItem, SubMenu } from 'react-pro-sidebar';
import { useNavigate, useLocation } from "react-router-dom";
import { FaCalendarAlt } from "react-icons/fa";
import { useAuth } from "../../context/UseAuth";
import Can from "../../rule/Can";

const menuItems: any[] = [
    { "key": "home", "url": "/home", "permissions": "", "name": "หน้าหลัก" },
    // { "key": "case-list", "url": "/case-list", "permissions": "", "name": "รายการเคส" },
];

const CustomSidebar = (props: any) => {
    const navigate = useNavigate();
    const location = useLocation();
    const { profile } = useAuth();

    return (
        <Sidebar className='col-auto col-md-3 col-xl-2 px-0 bg-white shadow-sm'>
            <Menu
                menuItemStyles={{
                    button: ({ level, active, disabled }) => {
                        return {
                            color: active ? '#505050' : '#8a8a8a',
                            backgroundColor: active ? '#ececec' : undefined,
                        };
                    },
                }}
            >
                {menuItems.map((menuItem, index) => (
                    <Can
                        perform={`${menuItem.permissions}`}
                        rules={`${profile().permission}`}
                        yes={() => (
                            <MenuItem
                                active={location.pathname.includes(menuItem.key)}
                                key={menuItem.key}
                                onClick={() => {
                                    navigate(menuItem.url);
                                }}
                            >
                                {menuItem.name}
                            </MenuItem>
                        )}
                    />
                ))}
            </Menu>
        </Sidebar>
    );
};

export default CustomSidebar;
