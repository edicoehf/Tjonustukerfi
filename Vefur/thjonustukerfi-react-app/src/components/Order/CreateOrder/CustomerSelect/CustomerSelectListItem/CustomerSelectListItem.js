import React from "react";
import { ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import AddCircleOutlineIcon from "@material-ui/icons/AddCircleOutline";
import { addCustomerType, customerType } from "../../../../../types";

const CustomerSelectListItem = ({ customer, addCustomer }) => {
    return (
        <ListItem
            button
            onClick={() => addCustomer(customer)}
            className="customer-select-list-item"
        >
            <ListItemText primary={customer.name} secondary={customer.email} />
            <ListItemIcon>
                <AddCircleOutlineIcon />
            </ListItemIcon>
        </ListItem>
    );
};

CustomerSelectListItem.propTypes = {
    customer: customerType,
    addCustomer: addCustomerType,
};

export default CustomerSelectListItem;
