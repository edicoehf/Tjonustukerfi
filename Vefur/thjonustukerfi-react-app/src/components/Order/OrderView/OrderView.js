import React from "react";
import OrderDetails from "../OrderDetails/OrderDetails";

const OrderView = ({ match }) => {
    const id = match.params.id;

    return (
        <div className="order-view">
            <div className="order">
                <OrderDetails id={id} />
            </div>
        </div>
    );
};

export default OrderView;
