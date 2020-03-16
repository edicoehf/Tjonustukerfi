import React from "react";
import Navbar from "react-bootstrap/Navbar";
import NavigationLinks from "../NavigationLinks/NavigationLinks";
import Logo from "../../../pictures/Reykofninn.logo.png";

const NavigationBar = () => {
    return (
        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
            <Navbar.Brand href="/">
                <img
                    src={Logo}
                    style={{ width: 50, marginLeft: 5, marginRight: 5 }}
                />
                Reykofninn
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
                <NavigationLinks />
            </Navbar.Collapse>
        </Navbar>
    );
};

export default NavigationBar;
