import React from "react";
import PropTypes from "prop-types";

const OrderItem = ({ category, service, barcode }) => {
    return (
        <tr className="order-item">
            <td className="order-item-category">{category}</td>
            <td className="order-item-service">{service}</td>
            <td className="order-item-barcode">{barcode}</td>
        </tr>
    );
};

OrderItem.propTypes = {
    category: PropTypes.string.isRequired,
    service: PropTypes.string.isRequired,
    barcode: PropTypes.string.isRequired,
};

export default OrderItem;
