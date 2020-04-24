import React from "react";
import { ListItem } from "@material-ui/core";
import "./OrderListItem.css";
import { orderType } from "../../../types/index";
import ListItemLink from "../../ListItemLink/ListItemLink";
import DeleteOrderAction from "../Actions/DeleteOrderAction/DeleteOrderAction";

const OrderListItem = ({ order }) => {
    return (
        <>
            <ListItemLink href={"/order/" + order.id}>{order.id}</ListItemLink>
            <ListItemLink href={"/order/" + order.id}>
                {order.customer + " og mail?"}
            </ListItemLink>
            <ListItemLink href={"/order/" + order.id}>
                {order.items.length}
            </ListItemLink>
            <ListItem className="order-button-area">
                <DeleteOrderAction />
            </ListItem>
        </>
    );
};

OrderListItem.propTypes = {
    order: orderType,
};

export default OrderListItem;
