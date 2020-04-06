import React from "react";
import OrderDetails from "../OrderDetails/OrderDetails";
import OrderActions from "../Actions/OrderActions/OrderActions";

import "./OrderView.css";
const OrderView = ({ match }) => {
    const id = match.params.id;

    return (
        <div className="order-view">
            <h1>Upplýsingar um pöntun</h1>
            <OrderDetails id={id} />
            <OrderActions id={id} />
        </div>
    );
};

export default OrderView;
