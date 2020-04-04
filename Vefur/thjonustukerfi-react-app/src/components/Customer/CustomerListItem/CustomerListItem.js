import React from "react";
import { ListGroup } from "react-bootstrap";
import CustomerActions from "../Actions/CustomerActions/CustomerActions";
import "./CustomerListItem.css";

const CustomerListItem = props => {
    return (
        <ListGroup.Item
            className="item"
            variant="light"
            action
            href={"/customer/" + props.customer.id}
        >
            {props.customer.name}
            <CustomerActions id={props.customer.id} />
        </ListGroup.Item>
    );
};
export default CustomerListItem;
