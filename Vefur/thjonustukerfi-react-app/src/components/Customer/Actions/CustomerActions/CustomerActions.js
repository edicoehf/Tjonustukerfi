import React from "react";
import DeleteCustomerAction from "../DeleteCustomerAction/DeleteCustomerAction";
import UpdateCustomerAction from "../UpdateCustomerAction/UpdateCustomerAction";
import "./CustomerActions.css";

const CustomerActions = ({ id }) => {
    return (
        <div className="customer-actions">
            <DeleteCustomerAction id={id} />
            <UpdateCustomerAction id={id} />
        </div>
    );
};

export default CustomerActions;
