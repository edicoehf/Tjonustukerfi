import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import OrderItemList from "../OrderItemList/OrderItemList";
import useGetOrderById from "../../../hooks/useGetOrderById";
import moment from "moment";
import Moment from "react-moment";

const OrderDetails = ({ id }) => {
    const { order, error } = useGetOrderById(id);

    // Icelandic human readable format, e.g. 4. sep, 2020 08:
    Moment.globalMoment = moment;
    Moment.globalLocale = "is";
    Moment.globalFormat = "lll";

    return (
        <div className="order-details">
            {!error ? (
                <>
                    <div className="order-title">Pöntun: {order.id}</div>
                    <div className="order-barcode">
                        Strikamerki: {order.barcode}
                    </div>
                    <div className="order-date">
                        Dagsetning: <Moment>{order.dateCreated}</Moment>
                    </div>
                    {order.dateCompleted && (
                        <div className="order-completed">
                            Sótt: <Moment>{order.dateCompleted}</Moment>
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
                type: PropTypes.string.isRequired,
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
