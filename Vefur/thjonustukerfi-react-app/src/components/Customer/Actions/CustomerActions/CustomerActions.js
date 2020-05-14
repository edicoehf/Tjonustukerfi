import React from "react";
import DeleteCustomerAction from "../DeleteCustomerAction/DeleteCustomerAction";
import UpdateCustomerAction from "../UpdateCustomerAction/UpdateCustomerAction";
import "./CustomerActions.css";
import { idType, handleOpenType } from "../../../../types/index";
import ViewCustomerOrdersAction from "../ViewCustomerOrdersAction/ViewCustomerOrdersAction";

/**
 * A row of actions (buttons) available for a Customer
 *
 * @component
 * @category Customer
 */

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
    /** Customer ID */
    id: idType,
    /** Callback function which is triggered by ViewCustomerOrdersAction */
    handleOpen: handleOpenType,
};

export default CustomerActions;
