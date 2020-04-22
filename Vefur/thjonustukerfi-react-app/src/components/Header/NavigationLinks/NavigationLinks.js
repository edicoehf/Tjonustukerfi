import React from "react";
import { Nav, NavDropdown } from "react-bootstrap";

const NavigationLinks = () => {
    return (
        <>
            <Nav className="mr-auto">
                <NavDropdown title="Pantanir" id="collasible-nav-dropdown">
                    <NavDropdown.Item href="/orders">
                        Leita að pöntun
                    </NavDropdown.Item>
                    <NavDropdown.Item href="/new-order">
                        Ný pöntun
                    </NavDropdown.Item>
                </NavDropdown>
                <NavDropdown
                    title="Vidskiptavinir"
                    id="collasible-nav-dropdown"
                >
                    <NavDropdown.Item href="/customers">
                        Leita að viðskiptavin
                    </NavDropdown.Item>
                    <NavDropdown.Item href="/new-customer">
                        Nýr viðskiptavinur
                    </NavDropdown.Item>
                </NavDropdown>
            </Nav>
        </>
    );
};

export default NavigationLinks;
