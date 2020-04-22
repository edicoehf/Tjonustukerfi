import React from "react";
import DeleteOrderAction from "../DeleteOrderAction/DeleteOrderAction";
import { idType } from "../../../../types";

const OrderActions = ({ id }) => {
    return (
        <div className="order-actions">
            <DeleteOrderAction id={id} />
        </div>
    );
};

OrderActions.propTypes = {
    id: idType,
};

export default OrderActions;
