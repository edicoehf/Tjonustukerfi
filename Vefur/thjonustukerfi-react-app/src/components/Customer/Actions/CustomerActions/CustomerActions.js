import React from "react";
import DeleteCustomerAction from "../DeleteCustomerAction/DeleteCustomerAction";
import UpdateCustomerAction from "../UpdateCustomerAction/UpdateCustomerAction";
import "./CustomerActions.css";
import { idType } from "../../../../types/index";

const CustomerActions = ({ id }) => {
    return (
        <div className="customer-actions">
            <DeleteCustomerAction id={id} />
            <UpdateCustomerAction id={id} />
        </div>
    );
};

CustomerActions.propTypes = {
    id: idType,
};

export default CustomerActions;
