import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import OrderItemList from "../OrderItemList/OrderItemList";
import useGetOrderById from "../../../hooks/useGetOrderById";
import moment from "moment";
import "moment/locale/is";
import "./OrderDetails.css";

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
    order: PropTypes.shape({
        id: PropTypes.number.isRequired,
        customer: PropTypes.string.isRequired,
        customerId: PropTypes.string.isRequired,
        barcode: PropTypes.string.isRequired,
        items: PropTypes.arrayOf(
            PropTypes.shape({
                id: PropTypes.number.isRequired,
                category: PropTypes.string.isRequired,
                service: PropTypes.string.isRequired,
                barcode: PropTypes.string.isRequired,
            })
        ),
        dateCreated: PropTypes.string.isRequired,
        dateModified: PropTypes.string.isRequired,
        dateCompleted: PropTypes.string.isRequired,
    }),
};

export default OrderDetails;
