import React from "react";
import CustomerListItem from "../CustomerListItem/CustomerListItem";
import { List, ListItem } from "@material-ui/core";
import "./CustomerList.css";
import { customersType, isLoadingType } from "../../../types";

const CustomerList = ({ customers, error, isLoading }) => {
    return (
        <div>
            {!error ? (
                isLoading ? (
                    <p> Sæki viðskiptavini </p>
                ) : (
                    <List className="customer-list">
                        <ListItem className="item">
                            <h5>Nafn</h5>
                        </ListItem>
                        <ListItem className="item action-item">
                            <h5 className="actions">Aðgerðir</h5>
                        </ListItem>
                        {customers.map((customer) => (
                            <CustomerListItem
                                customer={customer}
                                key={customer.id}
                            />
                        ))}
                    </List>
                )
            ) : (
                <p className="error">
                    {" "}
                    Villa kom upp: Gat ekki sótt viðskiptavin
                </p>
            )}
        </div>
    );
};

CustomerList.propTypes = {
    customers: customersType,
    isLoading: isLoadingType,
};

export default CustomerList;
