import React from "react";
import { Nav, NavDropdown } from "react-bootstrap";

const NavigationLinks = () => {
    return (
        <>
            <Nav className="mr-auto">
                <Nav.Link href="#pricing">Pantanir</Nav.Link>
                <NavDropdown
                    title="Vidskiptavinir"
                    id="collasible-nav-dropdown"
                >
                    <NavDropdown.Item href="/new-customer">
                        Skra nyjan vidskiptavin
                    </NavDropdown.Item>
                </NavDropdown>
            </Nav>
        </>
    );
};

export default NavigationLinks;
