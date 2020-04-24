import React from "react";
import DeleteOrderAction from "../DeleteOrderAction/DeleteOrderAction";
import { idType } from "../../../../types";
import UpdateOrderAction from "../UpdateOrderAction/UpdateOrderAction";
import "./OrderActions.css";

const OrderActions = ({ id }) => {
    return (
        <div className="order-actions">
            <DeleteOrderAction id={id} />
            <UpdateOrderAction id={id} />
        </div>
    );
};

OrderActions.propTypes = {
    id: idType,
};

export default OrderActions;
