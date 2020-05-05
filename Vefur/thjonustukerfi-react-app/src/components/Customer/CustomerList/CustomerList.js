import React from "react";
import CustomerListItem from "../CustomerListItem/CustomerListItem";
import { List, Divider, Paper } from "@material-ui/core";
import "./CustomerList.css";
import { customersType, isLoadingType } from "../../../types";
import PersonIcon from "@material-ui/icons/Person";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
const CustomerList = ({ customers, error, isLoading, cb }) => {
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
    customers: customersType,
    isLoading: isLoadingType,
};

export default CustomerList;
