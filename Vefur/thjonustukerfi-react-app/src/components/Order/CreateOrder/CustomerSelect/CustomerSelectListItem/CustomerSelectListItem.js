import React from "react";
import { ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import AddCircleOutlineIcon from "@material-ui/icons/AddCircleOutline";

const CustomerSelectListItem = ({ customer }) => {
    return (
        <ListItem className="customer-select-list-item">
            <ListItemIcon>
                <AddCircleOutlineIcon />
            </ListItemIcon>
            <ListItemText primary={customer.name} secondary={customer.email} />
        </ListItem>
    );
};

export default CustomerSelectListItem;
