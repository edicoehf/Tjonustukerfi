import React from "react";
import { itemType } from "../../../types/index";
import { useHistory } from "react-router-dom";
import { TableRow, TableCell } from "@material-ui/core";

const OrderItem = ({ item, border }) => {
    const { id, category, service, barcode, state, json, details } = item;
    const { sliced, filleted, otherCategory, otherService } = json;

    const history = useHistory();

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
                {filleted ? "Flakað" : "Óflakað"}
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
        </TableRow>
    );
};

OrderItem.propTypes = {
    item: itemType,
};

export default OrderItem;
