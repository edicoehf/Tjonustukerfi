import React from "react";
import DeleteOrderAction from "../DeleteOrderAction/DeleteOrderAction";
import { idType } from "../../../../types";
import UpdateOrderAction from "../UpdateOrderAction/UpdateOrderAction";
import "./OrderActions.css";
import CheckoutOrderAction from "../CheckoutOrderAction/CheckoutOrderAction";

const OrderActions = ({ id, hasUpdated }) => {
    return (
        <div className="order-actions">
            <DeleteOrderAction id={id} />
            <UpdateOrderAction id={id} />
            <CheckoutOrderAction id={id} hasUpdated={hasUpdated} />
        </div>
    );
};

OrderActions.propTypes = {
    id: idType,
};

export default OrderActions;
