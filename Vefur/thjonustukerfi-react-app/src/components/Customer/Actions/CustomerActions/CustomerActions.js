import React from "react";
import DeleteCustomerAction from "../DeleteCustomerAction/DeleteCustomerAction";

const CustomerActions = ({ id }) => {
    return (
        <div className="customer-actions">
            <DeleteCustomerAction id={id} />
            {/* <ModifyCustomer customer={customer} /> */}
        </div>
    );
};

export default CustomerActions;
