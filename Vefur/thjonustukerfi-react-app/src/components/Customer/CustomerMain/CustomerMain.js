import React from "react";
import CustomerList from "../CustomerList/CustomerList";
import "./CustomerMain.css";
import SearchBar from "../../SearchBar/SearchBar";
import useGetAllCustomers from "../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../hooks/useSearchBar";
import CreateCustomerAction from "../Actions/CreateCustomerAction/CreateCustomerAction";

const CustomerMain = () => {
    const { customers, error, isLoading } = useGetAllCustomers();
    customers.sort((a, b) => a.name.localeCompare(b.name));
    const { searchResults, handleChange, searchTerm } = useSearchBar(customers);
    const searchBarPlaceHolder = "Leita eftir nafni";

    return (
        <div className="customer-main">
            <div className="main-item header">
                <h1>Viðskiptavinir</h1>
            </div>
            <div className="main-item create-button">
                <CreateCustomerAction />
            </div>
            {!customers.length > 0 ? (
                <>
                    <h4 className="main-item no-customers">
                        Enginn viðskiptavinur fannst. Má bjóða þér að bæta við
                        viðskiptavin?{" "}
                    </h4>{" "}
                </>
            ) : (
                <>
                    <div className="main-item search-bar">
                        <SearchBar
                            searchTerm={searchTerm}
                            handleChange={handleChange}
                            placeHolder={searchBarPlaceHolder}
                            htmlId="customer-searchbar"
                        />
                    </div>
                    <div className="main-item">
                        <CustomerList
                            customers={searchResults}
                            error={error}
                            isLoading={isLoading}
                        />
                    </div>
                </>
            )}
        </div>
    );
};

export default CustomerMain;
