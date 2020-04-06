import React from "react";
import PropTypes from "prop-types";

const OrderItem = ({ type, service, barcode }) => {
    return (
        <tr className="order-item">
            <td className="order-item-type">{type}</td>
            <td className="order-item-service">{service}</td>
            <td className="order-item-barcode">{barcode}</td>
        </tr>
    );
};

OrderItem.propTypes = {
    type: PropTypes.string.isRequired,
    service: PropTypes.string.isRequired,
    barcode: PropTypes.string.isRequired,
};

export default OrderItem;
