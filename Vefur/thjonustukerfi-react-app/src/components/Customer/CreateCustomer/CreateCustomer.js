import React from "react";
import "./CreateCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";

const CreateCustomer = () => {
    return (
        <div className="body">
            <div className="header">
                <h1>Nýr viðskiptavinur</h1>
            </div>
            <div className="body">
                <CustomerInputForm />
            </div>
        </div>
    );
};

export default CreateCustomer;
