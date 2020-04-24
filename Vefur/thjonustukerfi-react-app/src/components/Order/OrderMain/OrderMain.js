import React from "react";
import useGetAllOrders from "../../../hooks/useGetAllOrders";
import OrderList from "../OrderList/OrderList";
import "./OrderMain.css";
import { ordersType, isLoadingType } from "../../../types";
import CreateOrderActions from "../Actions/CreateOrderAction/CreateOrderAction";

const OrderMain = () => {
    const { orders, error, isLoading } = useGetAllOrders();

    return (
        <div className="main-container">
            <div className="main-item header">
                <h1>Pantanir</h1>
            </div>
            <div className="main-item create-button">
                <CreateOrderActions />
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

OrderMain.propTypes = {
    orders: ordersType,
    isLoading: isLoadingType,
};

export default OrderMain;
