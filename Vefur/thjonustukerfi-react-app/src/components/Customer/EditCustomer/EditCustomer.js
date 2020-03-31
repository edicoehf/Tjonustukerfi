import React from "react";
import "./EditCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";

const EditCustomer = ({ id }) => {
    return (
        <div className="body">
            <div className="header">
                <h1>Breyta viðskiptavin</h1>
            </div>
            <div className="body">
                <CustomerInputForm />
            </div>
        </div>
    );
};

export default EditCustomer;
