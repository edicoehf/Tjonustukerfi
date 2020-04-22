import React from "react";
import { Link } from "react-router-dom";
import OrderItemList from "../OrderItemList/OrderItemList";
import useGetOrderById from "../../../hooks/useGetOrderById";
import moment from "moment";
import "moment/locale/is";
import "./OrderDetails.css";
import { orderType } from "../../../types/index";

const OrderDetails = ({ id }) => {
    const { order, error } = useGetOrderById(id);

    // Icelandic human readable format, e.g. 4. sep, 2020 08:
    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("lll");
    };
    return (
        <div className="order-details">
            {!error ? (
                <>
                    <div className="order-title">Pöntun {order.id}</div>
                    <div className="order-barcode">
                        Strikamerki: {order.barcode}
                    </div>
                    <div className="order-date">
                        Dagsetning: {dateFormat(order.dateCreated)}
                    </div>
                    {order.dateCompleted && (
                        <div className="order-completed">
                            Sótt: {dateFormat(order.dateCompleted)}
                        </div>
                    )}
                    <div className="order-customer">
                        Viðskiptavinur:{" "}
                        <Link to={`/customer/${order.customerId}`}>
                            {order.customer}
                        </Link>
                    </div>
                    <div className="order-items">
                        <div className="order-items-title">Vörur:</div>
                        <OrderItemList items={order.items} />
                    </div>
                </>
            ) : (
                <p className="error">Villa kom upp: Gat ekki sótt pöntun</p>
            )}
        </div>
    );
};

OrderDetails.propTypes = {
    order: orderType,
};

export default OrderDetails;
