import React from "react";
import { useLocation } from "react-router-dom";
import { MenuList, MenuItem } from "@material-ui/core";
import { Link } from "react-router-dom";
import "./NavigationLinks.css";

/**
 * Links to be displayed in the navbar
 *
 * @component
 * @category Navigation
 */

const NavigationLinks = () => {
    const router = useLocation();
    return (
        <MenuList className="navbar-items">
            <Link className="navbar-item" to="/orders">
                <MenuItem selected={router.pathname === "/orders"}>
                    Pantanir
                </MenuItem>
            </Link>
            <Link className="navbar-item" to="/new-order">
                <MenuItem selected={router.pathname === "/new-order"}>
                    Ný pöntun
                </MenuItem>
            </Link>
            <Link className="navbar-item" to="/customers">
                <MenuItem selected={router.pathname === "/customers"}>
                    Viðskiptavinir
                </MenuItem>
            </Link>
            <Link className="navbar-item" to="/new-customer">
                <MenuItem selected={router.pathname === "/new-customer"}>
                    Nýr viðskiptavinur
                </MenuItem>
            </Link>
            <Link className="navbar-item" to="/item-search">
                <MenuItem selected={router.pathname === "/item-search"}>
                    Leita að vöru
                </MenuItem>
            </Link>
            <Link className="navbar-item" to="/archives">
                <MenuItem selected={router.pathname === "/archives"}>
                    Skjalasafn
                </MenuItem>
            </Link>
        </MenuList>
    );
};

export default NavigationLinks;
