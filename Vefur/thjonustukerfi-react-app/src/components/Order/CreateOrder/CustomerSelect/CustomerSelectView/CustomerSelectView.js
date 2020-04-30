import React from "react";
import useGetAllCustomers from "../../../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../../../hooks/useSearchBar";
import SearchBar from "../../../../SearchBar/SearchBar";
import { List, Paper, Divider } from "@material-ui/core";
import CustomerListItem from "../../../../Customer/CustomerListItem/CustomerListItem";
import { addCustomerType } from "../../../../../types";
import AddIcon from "@material-ui/icons/AddCircleOutline";
import "./CustomerSelectView.css";

const CustomerSelectView = ({ addCustomer }) => {
    const { customers, error, isLoading } = useGetAllCustomers();
    customers.sort((a, b) => a.name.localeCompare(b.name));
    const { searchResults, handleChange, searchTerm } = useSearchBar(customers);
    const searchBarPlaceHolder = "Leita eftir nafni";

    return (
        <>
            {!error ? (
                isLoading ? (
                    <p> Sæki viðskiptavini </p>
                ) : (
                    <div className="customer-select-view">
                        <SearchBar
                            searchTerm={searchTerm}
                            handleChange={handleChange}
                            placeHolder={searchBarPlaceHolder}
                            htmlId="customer-select-searchbar"
                        />
                        {searchResults.length === 0 ? (
                            <p className="error no-customers">
                                Enginn viðskiptavinur fannst
                            </p>
                        ) : (
                            <Paper
                                elevation={3}
                                className="customer-select-list"
                            >
                                <List className="list-of-customers">
                                    {searchResults.map((customer, i) => (
                                        <React.Fragment key={customer.id}>
                                            <CustomerListItem
                                                customer={customer}
                                                onClick={addCustomer}
                                                icon={<AddIcon />}
                                            />
                                            {i < customers.length - 1 && (
                                                <Divider />
                                            )}
                                        </React.Fragment>
                                    ))}
                                </List>
                            </Paper>
                        )}
                    </div>
                )
            ) : (
                <p className="error">
                    {" "}
                    Villa kom upp: Gat ekki sótt viðskiptavin
                </p>
            )}
        </>
    );
};

CustomerSelectView.propTypes = {
    addCustomer: addCustomerType,
};

export default CustomerSelectView;
