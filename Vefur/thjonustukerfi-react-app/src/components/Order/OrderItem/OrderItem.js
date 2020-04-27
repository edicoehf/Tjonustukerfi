import React from "react";
import { itemType } from "../../../types/index";
import { Link } from "react-router-dom";
import { TableRow, TableCell } from "@material-ui/core";

const OrderItem = ({ item }) => {
    const { id, category, service, barcode, state, json } = item;
    const { slices } = json;
  
    return (
        <TableRow className="order-item">
            <TableCell className="order-item-id">
                <Link to={`/item/${id}`}>{id}</Link>
            </TableCell>
            <TableCell className="order-item-category">{category}</TableCell>
            <TableCell className="order-item-service">{service}</TableCell>
            <TableCell className="order-item-slices">{slices}</TableCell>
            <TableCell className="order-item-barcode">{barcode}</TableCell>
            <TableCell className="order-item-state">{state}</TableCell>
        </TableRow>
    );
};

OrderItem.propTypes = {
    item: itemType,
};

export default OrderItem;
