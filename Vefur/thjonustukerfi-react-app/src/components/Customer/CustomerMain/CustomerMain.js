import React from "react";
import CustomerList from "../CustomerList/CustomerList";
import "./CustomerMain.css";
import SearchBar from "../../SearchBar/SearchBar";
import useGetAllCustomers from "../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../hooks/useSearchBar";
import { Paper } from "@material-ui/core";

const CustomerMain = ({ history }) => {
    const { customers, error, isLoading } = useGetAllCustomers();
    customers.sort((a, b) => a.name.localeCompare(b.name));
    const { searchResults, handleChange, searchTerm } = useSearchBar(customers);
    const searchBarPlaceHolder = "Leita eftir nafni";

    const redirectToCustomerDetails = (id) => {
        history.push(`/customer/${id}`);
    };

    return (
        <div className="customer-main">
            <div className="main-item header">
                <h1>Viðskiptavinir</h1>
            </div>
            <Paper elevation={3} className="customer-search-paper">
                <SearchBar
                    searchTerm={searchTerm}
                    handleChange={handleChange}
                    placeHolder={searchBarPlaceHolder}
                    htmlId="customer-searchbar"
                />
            </Paper>
            {!isLoading && customers.length === 0 && !error ? (
                <p className="error">Engir viðskiptavinir í gagnagrunni</p>
            ) : (
                !isLoading &&
                searchResults.length === 0 &&
                !error && (
                    <p className="error">
                        Enginn viðskiptavinur fannst með þessum leitarskilyrðum
                    </p>
                )
            )}
            <CustomerList
                customers={searchResults}
                error={error}
                isLoading={isLoading}
                cb={redirectToCustomerDetails}
            />
        </div>
    );
};

export default CustomerMain;
