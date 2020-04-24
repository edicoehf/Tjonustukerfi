import React from "react";
import CustomerActions from "../Actions/CustomerActions/CustomerActions";
import "./CustomerListItem.css";
import { customerType } from "../../../types/index";
import ListItemLink from "../../ListItemLink/ListItemLink";
import { ListItem } from "@material-ui/core";

const CustomerListItem = ({ customer }) => {
    return (
        <>
            <ListItemLink href={"/customer/" + customer.id}>
                {customer.name} - {customer.email}
            </ListItemLink>
            <ListItem className="button-area">
                <CustomerActions id={customer.id} />
            </ListItem>
        </>
    );
};

CustomerListItem.propTypes = {
    customer: customerType,
};

export default CustomerListItem;
