import React from "react";
import PropTypes from "prop-types";

const OrderItem = ({ type, service, barcode }) => {
    return (
        <li className="order-item">
            <div className="order-item-type">{type}</div>
            <div className="order-item-service">{service}</div>
            <div className="order-item-barcode">{barcode}</div>
        </li>
    );
};

OrderItem.propTypes = {
    type: PropTypes.string.isRequired,
    service: PropTypes.string.isRequired,
    barcode: PropTypes.string.isRequired
};

export default OrderItem;
