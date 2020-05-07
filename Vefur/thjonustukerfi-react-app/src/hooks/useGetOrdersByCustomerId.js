import { useState, useEffect } from "react";
import customerService from "../services/customerService";

const useGetOrdersByCustomerId = (customerId) => {
    const [orders, setOrders] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        customerService
            .getOrdersByCustomerId(customerId)
            .then((orders) => {
                setOrders(orders);
                setError(null);
            })
            .catch((error) => {
                setError(error);
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, [customerId]);
    return { orders, isLoading, error };
};

export default useGetOrdersByCustomerId;
