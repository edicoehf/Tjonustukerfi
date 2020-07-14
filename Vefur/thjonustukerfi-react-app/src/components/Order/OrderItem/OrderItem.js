import React from "react";
import { itemType, borderType } from "../../../types/index";
import { useHistory } from "react-router-dom";
import { TableRow, TableCell } from "@material-ui/core";

/**
 * A row representing an item of an order.
 *
 * @component
 * @category Order
 */
const OrderItem = ({ item, border }) => {
    // Destruct item to get its properties
    const { id, category, service, barcode, state, json, details, quantity } = item;
    // Destruct json to get extra info
    const { sliced, filleted, otherCategory, otherService } = json;

    // Get history
    const history = useHistory();
    // Send user to item details page
    const handleRedirect = () => {
        history.push(`/item/${id}`);
    };

    return (
        <TableRow
            className={`order-item-list-row body-row ${
                border === true ? "with-border" : ""
            }`}
            onClick={handleRedirect}
            hover
        >
            <TableCell className="order-item-id">{id}</TableCell>
            <TableCell className="order-item-category">
                {otherCategory ? otherCategory : category}
            </TableCell>
            <TableCell className="order-item-service">
                {otherService ? otherService : service}
            </TableCell>
            <TableCell className="order-item-filleted">
                {filleted ? "Já" : "Nei"}
            </TableCell>
            <TableCell className="order-item-sliced">
                {sliced ? "Bitar" : "Heilt Flak"}
            </TableCell>
            <TableCell className="order-item-barcode">{barcode}</TableCell>
            {details !== "" && (
                <TableCell className="order-item-details">
                    <>
                        <b>Annað: </b>
                        {details}
                    </>
                </TableCell>
            )}
            <TableCell className="order-item-state">{state}</TableCell>
            <TableCell className="order-item-quantity">{quantity}</TableCell>
        </TableRow>
    );
};

OrderItem.propTypes = {
    /** Item to represent */
    item: itemType,
    /** Should row display a top-border */
    border: borderType,
};

export default OrderItem;
