import React from "react";
import { Link } from "react-router-dom";

import CustomerList from "../CustomerList/CustomerList";
import "./CustomerMain.css";
import SearchBar from "../../SearchBar/SearchBar";
import useGetAllCustomers from "../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../hooks/useSearchBar";

const CustomerMain = () => {
    const { customers, error, isLoading } = useGetAllCustomers();
    customers.sort((a, b) => a.name.localeCompare(b.name));
    const { searchResults, handleChange, searchTerm } = useSearchBar(customers);
    const searchBarPlaceHolder = "Má bjóða þér að leita eftir nafni?";

    return (
        <div className="main">
            <div className="main-item header">
                <h1>Viðskiptavinir</h1>
            </div>
            <div className="main-item create-button">
                <Link to="/new-customer" className="btn btn-lg btn-success">
                    Bæta við viðskiptavin
                </Link>
            </div>
            <div className="main-item search-bar">
                <SearchBar
                    searchTerm={searchTerm}
                    handleChange={handleChange}
                    placeHolder={searchBarPlaceHolder}
                />
            </div>
            <div className="main-item">
                <CustomerList
                    customers={searchResults}
                    error={error}
                    isLoading={isLoading}
                />
            </div>
        </div>
    );
};

export default CustomerMain;
