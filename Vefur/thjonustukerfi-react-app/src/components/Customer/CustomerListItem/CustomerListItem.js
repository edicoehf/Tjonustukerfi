import React from "react";
import { ListGroup } from "react-bootstrap";
import CustomerActions from "../Actions/CustomerActions/CustomerActions";
import "./CustomerListItem.css";
import { customerType } from "../../../types/index";

const CustomerListItem = ({ customer }) => {
    return (
        <>
            <ListGroup.Item
                variant="light"
                action
                href={"/customer/" + customer.id}
            >
                {customer.name} - {customer.email}
            </ListGroup.Item>
            <ListGroup.Item className="buttons">
                <CustomerActions id={customer.id} />
            </ListGroup.Item>
        </>
    );
};

CustomerListItem.propTypes = {
    customer: customerType,
};

export default CustomerListItem;
