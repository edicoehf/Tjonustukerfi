import React from "react";
import OrderItem from "../OrderItem/OrderItem";
import { Table } from "react-bootstrap";
import { itemsType } from "../../../types";

const OrderItemList = ({ items }) => {
    return (
        <Table className="order-item-list">
            <thead>
                <tr>
                    <th>Vörunúmer</th>
                    <th>Tegund</th>
                    <th>Þjónusta</th>
                    <th>Strikamerki</th>
                </tr>
            </thead>
            <tbody>
                {items.map((item) => (
                    <OrderItem key={item.id} item={item} />
                ))}
            </tbody>
        </Table>
    );
};

OrderItemList.propTypes = {
    items: itemsType,
};

export default OrderItemList;
