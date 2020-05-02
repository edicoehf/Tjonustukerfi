import React from "react";
import { ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { addCustomerType, customerType } from "../../../types";
import "./CustomerListItem.css";

const CustomerListItem = ({ customer, onClick, icon }) => {
    return (
        <ListItem
            button
            onClick={() => onClick(customer)}
            className="customer-list-item"
        >
            <ListItemText primary={customer.name} secondary={customer.email} />
            {icon && <ListItemIcon>{icon}</ListItemIcon>}
        </ListItem>
    );
};

CustomerListItem.propTypes = {
    customer: customerType,
    onClick: addCustomerType,
};

export default CustomerListItem;
