import React from "react";
import useGetAllCustomers from "../../../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../../../hooks/useSearchBar";
import SearchBar from "../../../../SearchBar/SearchBar";
import { List, Paper, Divider } from "@material-ui/core";
import CustomerListItem from "../../../../Customer/CustomerListItem/CustomerListItem";
import { addCustomerType } from "../../../../../types";
import AddIcon from "@material-ui/icons/AddCircleOutline";
import "./CustomerSelectView.css";
import ProgressComponent from "../../../../Feedback/ProgressComponent/ProgressComponent";

/**
 * A list of customers that can be selected for an order, in the create order process.
 * Made to be displayed in the pickcustomermodal.
 *
 * @component
 * @category Order
 */
const CustomerSelectView = ({ addCustomer }) => {
    // Get all the customers
    const { customers, error, isLoading } = useGetAllCustomers();
    // Order customers by name
    // customers.sort((a, b) => a.name.localeCompare(b.name));
    // Filter customers by searchbar input
    const { searchResults, handleChange, searchTerm } = useSearchBar(customers, "name,phone", 20);
    const searchBarPlaceHolder = "Leita eftir nafni eða síma";

    return (
        <>
            <div className="customer-select-view">
                <Paper elevation={3} className="customer-select-search-paper">
                    <SearchBar
                        searchTerm={searchTerm}
                        handleChange={handleChange}
                        placeHolder={searchBarPlaceHolder}
                        htmlId="customer-select-searchbar"
                    />
                </Paper>
                {isLoading ? (
                    <ProgressComponent isLoading={isLoading} />
                ) : error ? (
                    <p className="error">Gat ekki sótt viðskiptavini</p>
                ) : customers.length === 0 ? (
                    <p className="error">Engir viðskiptavinir í gagnagrunni</p>
                ) : (!searchResults || searchResults.length === 0) ? (
                    <p className="error">
                        Sláðu inn hluta úr nafni eða símanúmeri viðskiptavinar til að leita
                    </p>
                ) : ( searchResults.length === 0 ) ? (
                    <p className="error">
                        Engir viðskiptavinir fundust með þessum leitarskilyrðum
                    </p>
                ) : (
                    <Paper elevation={3} className="customer-select-list">
                        <List className="list-of-customers">
                            {searchResults.map((customer, i) => (
                                <React.Fragment key={customer.id}>
                                    <CustomerListItem
                                        customer={customer}
                                        onClick={addCustomer}
                                        icon={<AddIcon />}
                                    />
                                    {i < customers.length - 1 && <Divider />}
                                </React.Fragment>
                            ))}
                        </List>
                    </Paper>
                )}
            </div>
        </>
    );
};

CustomerSelectView.propTypes = {
    /** CB that adds a customer to an order */
    addCustomer: addCustomerType,
};

export default CustomerSelectView;
