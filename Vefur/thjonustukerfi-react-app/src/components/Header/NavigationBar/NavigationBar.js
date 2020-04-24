import React from "react";
import NavigationLinks from "../NavigationLinks/NavigationLinks";
import Logo from "../../../pictures/Reykofninn.logo.png";
import { AppBar, Toolbar } from "@material-ui/core";
import "./NavigationBar.css";
import { Link } from "react-router-dom";

const NavigationBar = () => {
    return (
        <AppBar position="static">
            <Toolbar className="app-bar">
                <Link className="logo" to="/new-order">
                    <img
                        alt="logo"
                        src={Logo}
                        style={{ width: 50, marginLeft: 5, marginRight: 5 }}
                    />
                    Reykofninn
                </Link>
                <NavigationLinks />
            </Toolbar>
        </AppBar>
    );
};

export default NavigationBar;
