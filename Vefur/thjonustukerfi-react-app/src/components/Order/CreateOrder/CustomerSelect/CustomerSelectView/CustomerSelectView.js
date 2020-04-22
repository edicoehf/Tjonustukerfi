import React from "react";
import useGetAllCustomers from "../../../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../../../hooks/useSearchBar";
import SearchBar from "../../../../SearchBar/SearchBar";
import { List } from "@material-ui/core";
import CustomerSelectListItem from "../CustomerSelectListItem/CustomerSelectListItem";
import { addCustomerType } from "../../../../../types";

const CustomerSelectView = ({ addCustomer }) => {
    const { customers, error, isLoading } = useGetAllCustomers();
    customers.sort((a, b) => a.name.localeCompare(b.name));
    const { searchResults, handleChange, searchTerm } = useSearchBar(customers);
    const searchBarPlaceHolder = "Má bjóða þér að leita eftir nafni?";

    return (
        <>
            {!error ? (
                isLoading ? (
                    <p> Sæki viðskiptavini </p>
                ) : (
                    <div className="customer-select-view">
                        <div className="search-bar">
                            <SearchBar
                                searchTerm={searchTerm}
                                handleChange={handleChange}
                                placeHolder={searchBarPlaceHolder}
                            />
                        </div>
                        {searchResults.length === 0 ? (
                            <p className="error no-customers">
                                Enginn viðskiptavinur fannst
                            </p>
                        ) : (
                            <List className="customer-select-list">
                                {searchResults.map((customer) => (
                                    <CustomerSelectListItem
                                        key={customer.id}
                                        customer={customer}
                                        addCustomer={addCustomer}
                                    />
                                ))}
                            </List>
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
