import React from "react";
import "./UpdateCustomer.css";
import CustomerInputForm from "../CustomerInputForm/CustomerInputForm";
import useUpdateCustomer from "../../../hooks/useUpdateCustomer";

const UpdateCustomer = () => {
    const { customer } = useContext(CustomerContext);
    const { error, handleUpdate, isProcessing } = useUpdateCustomer();

    return (
        <div className="body">
            <div className="header">
                <h1>Breyta viðskiptavin</h1>
            </div>
            <div className="body">
                <CustomerInputForm
                    processing={isProcessing}
                    existingCustomer={customer}
                    submitHandler={handleUpdate}
                />
            </div>
            {error && <div>Gat ekki breytt viðskiptavin</div>}
        </div>
    );
};

export default UpdateCustomer;
