import React from "react";
import { itemType } from "../../../types/index";
import { Link } from "react-router-dom";
import { TableRow, TableCell } from "@material-ui/core";

const OrderItem = ({ item }) => {
    const { id, category, service, barcode } = item;
    return (
        <TableRow className="order-item">
            <TableCell className="order-item-id">
                <Link to={`/item/${id}`}>{id}</Link>
            </TableCell>
            <TableCell className="order-item-category">{category}</TableCell>
            <TableCell className="order-item-service">{service}</TableCell>
            <TableCell className="order-item-barcode">{barcode}</TableCell>
        </TableRow>
    );
};

OrderItem.propTypes = {
    item: itemType,
};

export default OrderItem;
