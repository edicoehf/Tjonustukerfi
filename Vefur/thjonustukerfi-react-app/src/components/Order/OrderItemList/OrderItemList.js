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
    return (
        <Table className="order-item-list">
            <TableHead>
                <TableRow>
                    <TableCell>
                        <b>Vörunúmer</b>
                    </TableCell>
                    <TableCell>
                        <b>Tegund</b>
                    </TableCell>
                    <TableCell>
                        <b>Þjónusta</b>
                    </TableCell>
                    <TableCell>
                        <b>Flökun</b>
                    </TableCell>
                    <TableCell>
                        <b>Pökkun</b>
                    </TableCell>
                    <TableCell>
                        <b>Annað</b>
                    </TableCell>
                    <TableCell>
                        <b>Strikamerki</b>
                    </TableCell>
                    <TableCell>
                        <b>Staða</b>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {items.map((item) => (
                    <OrderItem key={item.id} item={item} />
                ))}
            </TableBody>
        </Table>
    );
};

OrderItemList.propTypes = {
    items: itemsType,
};

export default OrderItemList;
