import React from "react";
import "./EditCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";
import useUpdateCustomer from "../../../hooks/useUpdateCustomer";

const EditCustomer = () => {
    const { customer } = useContext(CustomerContext);
    const { error, handleUpdate, isUpdating } = useUpdateCustomer();

    return (
        <div className="body">
            <div className="header">
                <h1>Breyta viðskiptavin</h1>
            </div>
            <div className="body">
                <CustomerInputForm
                    processing={isUpdating}
                    existingCustomer={customer}
                    submitHandler={handleUpdate}
                />
            </div>
            {error && <div>Gat ekki breytt viðskiptavin</div>}
        </div>
    );
};

export default EditCustomer;
