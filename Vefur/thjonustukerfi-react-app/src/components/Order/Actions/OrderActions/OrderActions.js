import React from "react";
import DeleteOrderAction from "../DeleteOrderAction/DeleteOrderAction";

const OrderActions = ({ id }) => {
    return (
        <div className="order-actions">
            <DeleteOrderAction id={id} />
        </div>
    );
};

export default OrderActions;
