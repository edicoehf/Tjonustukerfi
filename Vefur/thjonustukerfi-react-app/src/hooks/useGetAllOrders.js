import { useState, useEffect } from "react";
import orderService from "../services/orderService";

const useGetAllOrders = () => {
    const [orders, setOrders] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        orderService
            .getAllOrders()
            .then(orders => {
                setOrders(orders);
                setIsLoading(false);
                setError(null);
            })
            .catch(error => {
                setError(error);
            });
    }, []);
    return { orders, isLoading, error };
};

export default useGetAllOrders;
