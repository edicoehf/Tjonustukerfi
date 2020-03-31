import React from "react";
import DeleteCustomer from "../DeleteCustomer/DeleteCustomer";

const CustomerActions = ({ id }) => {
    return (
        <div className="customer-actions">
            <DeleteCustomer id={id} />
            {/* <ModifyCustomer customer={customer} /> */}
        </div>
    );
};

export default CustomerActions;
