import React from "react";
import DeleteCustomer from "../DeleteCustomer/DeleteCustomer";

const CustomerActions = ({ customer }) => {
    return (
        <>
            {customer ? (
                <div className="customer-actions">
                    <DeleteCustomer id={customer.id} />
                    {/* <ModifyCustomer customer={customer} /> */}
                </div>
            ) : (
                <></>
            )}
        </>
    );
};

export default CustomerActions;
