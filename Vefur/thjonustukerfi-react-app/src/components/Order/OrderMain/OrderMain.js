import React from "react";
import { Link } from "react-router-dom";

import useGetAllOrders from "../../../hooks/useGetAllOrders";
import OrderList from "../OrderList/OrderList";

const OrderMain = () => {
    const { orders, error, isLoading } = useGetAllOrders();

    return (
        <div className="main">
            <div className="main-item header">
                <h1>Pantanir</h1>
            </div>
            <div className="main-item create-button">
                <Link to="/new-order" className="btn btn-lg btn-success">
                    Bæta við pöntun
                </Link>
            </div>
            <div className="main-item">
                <OrderList
                    orders={orders}
                    error={error}
                    isLoading={isLoading}
                />
            </div>
        </div>
    );
};

export default OrderMain;
