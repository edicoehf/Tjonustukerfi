import React from "react";
import OrderItem from "../OrderItem/OrderItem";
import PropTypes from "prop-types";

const OrderItemList = ({ items }) => {
    return (
        <ul className="order-item-list">
            {items.map(item => (
                <OrderItem
                    key={item.id}
                    type={item.type}
                    service={item.service}
                    barcode={item.barcode}
                />
            ))}
        </ul>
    );
};

OrderItemList.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            type: PropTypes.string.isRequired,
            service: PropTypes.string.isRequired,
            barcode: PropTypes.string.isRequired
        })
    )
};

export default OrderItemList;
