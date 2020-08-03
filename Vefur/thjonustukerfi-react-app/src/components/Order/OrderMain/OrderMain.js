import React from "react";
import useGetAllOrders from "../../../hooks/useGetAllOrders";
import OrderList from "../OrderList/OrderList";
import "./OrderMain.css";
import { Paper } from "@material-ui/core";
import SearchBar from "../../SearchBar/SearchBar";
import useSearchBar from "../../../hooks/useSearchBar";

/**
 * Page which displays list of all orders and a searchbar
 *
 * @component
 * @category Order
 */
const OrderMain = () => {
    // Get all orders
    const { orders, error, isLoading } = useGetAllOrders();
    // Use search bar hook, filter all orders using the searchbar input
    const { searchResults, handleChange, searchTerm } = useSearchBar(
        orders,
        "customer,id",
        30
    );

    return (
        <div className="order-main">
            <div className="main-item header">
                <h1>Pantanir</h1>
            </div>
            <div className="main-item">
                <Paper elevation={3} className="order-search-paper">
                    <SearchBar
                        searchTerm={searchTerm}
                        handleChange={handleChange}
                        placeHolder="Leita eftir nafni viðsk.vinar eða pnr."
                        htmlId="order-searchbar"
                    />
                </Paper>
                <OrderList
                    orders={searchResults}
                    error={error}
                    isLoading={isLoading}
                />
                {!isLoading && orders.length === 0 && !error ? (
                    <p className="error">Engar pantanir í gagnagrunni</p>
                ) : (
                    !isLoading &&
                    searchResults.length === 0 &&
                    !error && (
                        <p className="error">
                            Engar pantanir fundust með þessum leitarskilyrðum
                        </p>
                    )
                )}
            </div>
        </div>
    );
};

export default OrderMain;
