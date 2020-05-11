import React from "react";
import { Paper } from "@material-ui/core";
import useGetAllArchivedOrders from "../../../../hooks/useGetAllArchivedOrders";
import useSearchBar from "../../../../hooks/useSearchBar";
import SearchBar from "../../../SearchBar/SearchBar";
import ArchiveOrderList from "../ArchiveOrderList/ArchiveOrderList";
import { ordersType, isLoadingType } from "../../../../types";
import "./ArchiveOrderMain.css";

const ArchiveOrderMain = () => {
    const { orders, error, isLoading } = useGetAllArchivedOrders();
    const { searchResults, handleChange, searchTerm } = useSearchBar(
        orders,
        "customer"
    );

    return (
        <div className="order-archives-main">
            <div className="main-item header">
                <h1>Skjalasafn eldri pantana</h1>
            </div>
            <div className="main-item">
                <Paper elevation={3} className="order-archives-search-paper">
                    <SearchBar
                        searchTerm={searchTerm}
                        handleChange={handleChange}
                        placeHolder="Leita eftir nafni viðskiptavinar"
                        htmlId="order-archives-searchbar"
                    />
                </Paper>
                <ArchiveOrderList
                    orders={searchResults}
                    error={error}
                    isLoading={isLoading}
                />
                {!isLoading && orders.length === 0 && !error ? (
                    <p className="error">
                        Engar skjalaðar pantanir í gagnagrunni
                    </p>
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

ArchiveOrderMain.propTypes = {
    orders: ordersType,
    isLoading: isLoadingType,
};

export default ArchiveOrderMain;
