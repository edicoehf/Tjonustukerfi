import React from "react";
import CustomerListItem from "../CustomerListItem/CustomerListItem";
import { List, Divider, Paper } from "@material-ui/core";
import "./CustomerList.css";
import {
    customersType,
    isLoadingType,
    cbType,
    errorType,
} from "../../../types";
import PersonIcon from "@material-ui/icons/Person";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

/**
 * A list displaying given customers.
 * Clicking an item on the list will call the given CB function with the customer id as a parameter,
 * CB intended for opening a page with the customers details
 *
 * @component
 * @category Customer
 */

const CustomerList = ({ customers, error, isLoading, cb }) => {
    // Call the redirection CB with the customers id
    const redirect = (customer) => {
        cb(customer.id);
    };
    return (
        <div className="customer-list-view">
            {isLoading ? (
                <ProgressComponent isLoading={isLoading} />
            ) : !error ? (
                <Paper elevation={3} className="customer-list">
                    <List className="list-of-customers">
                        {customers.map((customer, i) => (
                            <React.Fragment key={customer.id}>
                                <CustomerListItem
                                    customer={customer}
                                    onClick={redirect}
                                    icon={<PersonIcon />}
                                />
                                {i < customers.length - 1 && <Divider />}
                            </React.Fragment>
                        ))}
                    </List>
                </Paper>
            ) : (
                <p className="error">Gat ekki sótt viðskiptavini</p>
            )}
        </div>
    );
};

CustomerList.propTypes = {
    /** List of customers */
    customers: customersType,
    /** Whether an error occured fetching fetching customers */
    error: errorType,
    /** Whether the customers are being fetched */
    isLoading: isLoadingType,
    /** Callback function to be called when customer is clicked
     * Should send user to customers detail page
     * @param id = Customer ID
     */
    cb: cbType,
};

export default CustomerList;
