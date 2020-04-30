import React from "react";
import { itemType } from "../../../types/index";
import { Link } from "react-router-dom";
import { TableRow, TableCell } from "@material-ui/core";

const OrderItem = ({ item }) => {
    const { id, category, service, barcode, state, json, details } = item;
    const { sliced, filleted, otherCategory, otherService } = json;

    return (
        <TableRow className="order-item">
            <TableCell className="order-item-id">
                <Link to={`/item/${id}`}>{id}</Link>
            </TableCell>
            <TableCell className="order-item-category">
                {otherCategory ? otherCategory : category}
            </TableCell>
            <TableCell className="order-item-service">
                {otherService ? otherService : service}
            </TableCell>
            <TableCell className="order-item-filleted">
                {filleted ? "Flakað" : "Óflakað"}
            </TableCell>
            <TableCell className="order-item-sliced">
                {sliced ? "Bitar" : "Heilt Flak"}
            </TableCell>
            <TableCell className="order-item-details">{details}</TableCell>
            <TableCell className="order-item-barcode">{barcode}</TableCell>
            <TableCell className="order-item-state">{state}</TableCell>
        </TableRow>
    );
};

OrderItem.propTypes = {
    item: itemType,
};

export default OrderItem;
