import React from "react";
import useGetAllOrders from "../../../hooks/useGetAllOrders";
import OrderList from "../OrderList/OrderList";
import "./OrderMain.css";
import { ordersType, isLoadingType } from "../../../types";
import { Paper } from "@material-ui/core";
import SearchBar from "../../SearchBar/SearchBar";
import useSearchBar from "../../../hooks/useSearchBar";

const OrderMain = () => {
    const { orders, error, isLoading } = useGetAllOrders();
    const { searchResults, handleChange, searchTerm } = useSearchBar(
        orders,
        "customer"
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
                        placeHolder="Leita eftir nafni viðskiptavins"
                        htmlId="customer-searchbar"
                    />
                </Paper>
                <OrderList
                    orders={searchResults}
                    error={error}
                    isLoading={isLoading}
                />
            </div>
        </div>
    );
};

OrderMain.propTypes = {
    orders: ordersType,
    isLoading: isLoadingType,
};

export default OrderMain;
