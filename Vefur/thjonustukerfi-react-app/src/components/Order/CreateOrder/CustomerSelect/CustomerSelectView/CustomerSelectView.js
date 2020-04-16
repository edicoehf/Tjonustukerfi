import React from "react";
import { Link } from "react-router-dom";
import useGetAllCustomers from "../../../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../../../hooks/useSearchBar";
import SearchBar from "../../../../SearchBar/SearchBar";
import { List } from "@material-ui/core";
import CustomerSelectListItem from "../CustomerSelectListItem/CustomerSelectListItem";

const CustomerSelectView = () => {
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
                        {!customers.length > 0 ? (
                            <h4 className="no-customers">
                                Enginn viðskiptavinur fannst. Má bjóða þér að
                                bæta við viðskiptavin?{" "}
                            </h4>
                        ) : (
                            <List className="customer-select-list">
                                {searchResults.map((customer) => (
                                    <CustomerSelectListItem
                                        key={customer.id}
                                        customer={customer}
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

export default CustomerSelectView;
