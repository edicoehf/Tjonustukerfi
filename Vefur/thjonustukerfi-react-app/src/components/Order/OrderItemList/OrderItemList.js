import React from "react";
import OrderItem from "../OrderItem/OrderItem";
import {
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
} from "@material-ui/core";
import { itemsType } from "../../../types";
import "./OrderItemList.css";

const OrderItemList = ({ items }) => {
    items = items.sort((a, b) => (a.id > b.id ? 1 : b.id > a.id ? -1 : 0));

    return (
        <Table className="order-item-list">
            <TableHead>
                <TableRow className="order-item-list-row">
                    <TableCell className="order-item-list-id">
                        <b>Vara</b>
                    </TableCell>
                    <TableCell className="order-item-category">
                        <b>Tegund</b>
                    </TableCell>
                    <TableCell className="order-item-service">
                        <b>Þjónusta</b>
                    </TableCell>
                    <TableCell className="order-item-filleted">
                        <b>Flökun</b>
                    </TableCell>
                    <TableCell className="order-item-sliced">
                        <b>Pökkun</b>
                    </TableCell>
                    <TableCell className="order-item-barcode">
                        <b>Strikamerki</b>
                    </TableCell>
                    <TableCell className="order-item-list-id">
                        <b>Staða</b>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {items.map((item, i) => (
                    <OrderItem key={i} item={item} border={i !== 0} />
                ))}
            </TableBody>
        </Table>
    );
};

OrderItemList.propTypes = {
    items: itemsType,
};

export default OrderItemList;
