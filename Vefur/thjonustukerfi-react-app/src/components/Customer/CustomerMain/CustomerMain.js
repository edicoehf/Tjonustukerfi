import React from "react";
import { Link } from "react-router-dom";

import CustomerList from "../CustomerList/CustomerList";
import "./CustomerMain.css";

const CustomerMain = () => {
    return (
        <div className="main">
            <div className="main-item header">
                <h1>Viðskiptavinir</h1>
            </div>
            <div className="main-item create-button">
                <Link to="/new-customer" className="btn btn-lg btn-success">
                    Bæta við viðskiptavin
                </Link>
            </div>
            <div className="main-item">
                <CustomerList />
            </div>
        </div>
    );
};

export default CustomerMain;
