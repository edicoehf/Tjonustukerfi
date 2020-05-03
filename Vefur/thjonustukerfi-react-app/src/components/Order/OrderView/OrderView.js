import React from "react";
import OrderDetails from "../OrderDetails/OrderDetails";
import OrderActions from "../Actions/OrderActions/OrderActions";

import "./OrderView.css";
const OrderView = ({ match }) => {
    const id = match.params.id;
    const [update, setUpdate] = React.useState(false);

    const hasUpdated = () => {
        setUpdate(true);
    };

    const receivedUpdate = () => {
        setUpdate(false);
    };

    return (
        <div className="order-view">
            <h1 className="order-detail-header">Upplýsingar um pöntun</h1>
            <OrderDetails
                id={id}
                update={update}
                receivedUpdate={receivedUpdate}
            />
            <OrderActions id={id} hasUpdated={hasUpdated} />
        </div>
    );
};

export default OrderView;
