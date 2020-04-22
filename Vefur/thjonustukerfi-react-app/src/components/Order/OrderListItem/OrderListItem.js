import React from "react";
import { ListGroup } from "react-bootstrap";
import "./OrderListItem.css";
import { orderType } from "../../../types/index";

const OrderListItem = ({ order }) => {
    return (
        <>
            <ListGroup.Item
                className="item"
                variant="light"
                action
                href={"/order/" + order.id}
            >
                {order.id}
            </ListGroup.Item>
            <ListGroup.Item
                className="item"
                variant="light"
                action
                href={"/order/" + order.id}
            >
                {order.customer}
            </ListGroup.Item>
            <ListGroup.Item
                className="item"
                variant="light"
                action
                href={"/order/" + order.id}
            >
                {order.items.length}
            </ListGroup.Item>
        </>
    );
};

OrderListItem.propTypes = {
    order: orderType,
};

export default OrderListItem;
