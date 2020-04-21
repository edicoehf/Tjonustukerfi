import React from "react";
import { itemType } from "../../../types/index";

const OrderItem = ({ item }) => {
    const { category, service, barcode } = item;
    return (
        <tr className="order-item">
            <td className="order-item-category">{category}</td>
            <td className="order-item-service">{service}</td>
            <td className="order-item-barcode">{barcode}</td>
        </tr>
    );
};

OrderItem.propTypes = {
    item: itemType,
};

export default OrderItem;
