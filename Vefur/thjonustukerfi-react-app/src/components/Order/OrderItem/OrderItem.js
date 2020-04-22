import React from "react";
import { itemType } from "../../../types/index";
import { Link } from "react-router-dom";

const OrderItem = ({ item }) => {
    const { id, category, service, barcode } = item;
    return (
        <Link to={`/item/${id}`}>
            <tr className="order-item">
                <td className="order-item-category">{category}</td>
                <td className="order-item-service">{service}</td>
                <td className="order-item-barcode">{barcode}</td>
            </tr>
        </Link>
    );
};

OrderItem.propTypes = {
    item: itemType,
};

export default OrderItem;
