import React from "react";
import DeleteOrderAction from "../DeleteOrderAction/DeleteOrderAction";
import { idType, cbType } from "../../../../types";
import UpdateOrderAction from "../UpdateOrderAction/UpdateOrderAction";
import "./OrderActions.css";
import CheckoutOrderAction from "../CheckoutOrderAction/CheckoutOrderAction";

/**
 * Display row of available actions (buttons) for an order
 *
 * @component
 * @category Order
 */
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
    /** Order ID */
    id: idType,
    /** CB telling parent that order has updated */
    hasUpdated: cbType,
};

export default OrderActions;
