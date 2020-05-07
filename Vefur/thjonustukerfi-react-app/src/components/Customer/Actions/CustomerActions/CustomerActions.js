import React from "react";
import DeleteCustomerAction from "../DeleteCustomerAction/DeleteCustomerAction";
import UpdateCustomerAction from "../UpdateCustomerAction/UpdateCustomerAction";
import "./CustomerActions.css";
import { idType } from "../../../../types/index";
import ViewCustomerOrdersAction from "../ViewCustomerOrdersAction/ViewCustomerOrdersAction";

const CustomerActions = ({ id, handleOpen }) => {
    return (
        <div className="customer-actions">
            <DeleteCustomerAction id={id} />
            <UpdateCustomerAction id={id} />
            <ViewCustomerOrdersAction handleOpen={handleOpen} />
        </div>
    );
};

CustomerActions.propTypes = {
    id: idType,
};

export default CustomerActions;
