import React from "react";

const OrderActions = ({ id }) => {
    return (
        <div className="order-actions">
            <DeleteOrderAction id={id} />
        </div>
    );
};

export default OrderActions;
