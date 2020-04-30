import React from "react";
import CustomerList from "../CustomerList/CustomerList";
import "./CustomerMain.css";
import SearchBar from "../../SearchBar/SearchBar";
import useGetAllCustomers from "../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../hooks/useSearchBar";
import CreateCustomerAction from "../Actions/CreateCustomerAction/CreateCustomerAction";

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
            <div className="main-item create-button">
                <CreateCustomerAction />
            </div>
            <SearchBar
                searchTerm={searchTerm}
                handleChange={handleChange}
                placeHolder={searchBarPlaceHolder}
                htmlId="customer-searchbar"
            />
            {!(searchResults.length > 0) && customers.length > 0 ? (
                <>
                    <h4 className="main-item no-customers">
                        Enginn viðskiptavinur fannst með þessum leitarskilyrðum
                    </h4>
                </>
            ) : (
                <>
                    <CustomerList
                        customers={searchResults}
                        error={error}
                        isLoading={isLoading}
                        cb={redirectToCustomerDetails}
                    />
                </>
            )}
        </div>
    );
};

export default CustomerMain;
