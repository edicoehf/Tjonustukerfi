import React from "react";
import { itemType } from "../../../types/index";
import { Link } from "react-router-dom";

const OrderItem = ({ item }) => {
    const { id, category, service, barcode } = item;
    return (
        <tr className="order-item">
            <td className="order-item-id">
                <Link to={`/item/${id}`}>{id}</Link>
            </td>
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
