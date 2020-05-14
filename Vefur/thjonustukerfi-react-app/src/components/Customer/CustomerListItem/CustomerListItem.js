import React from "react";
import { ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { addCustomerType, customerType, iconType } from "../../../types";
import "./CustomerListItem.css";

/**
 * ListItem for the CustomerList.
 * Represents a single customer in the list by displaying the customers name and email
 *
 * @component
 * @category Customer
 */

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
    /** Customer to represent */
    customer: customerType,
    /** Callback function to call onclick
     * @param {customerType} - Customer
     */
    onClick: addCustomerType,
    /** Decorative Icon to display in the listitem */
    icon: iconType,
};

export default CustomerListItem;
