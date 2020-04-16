import React from "react";
import { ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import AddCircleOutlineIcon from "@material-ui/icons/AddCircleOutline";

const CustomerSelectListItem = ({ customer }) => {
    return (
        <ListItem button className="customer-select-list-item">
            <ListItemText primary={customer.name} secondary={customer.email} />
            <ListItemIcon>
                <AddCircleOutlineIcon />
            </ListItemIcon>
        </ListItem>
    );
};

export default CustomerSelectListItem;
