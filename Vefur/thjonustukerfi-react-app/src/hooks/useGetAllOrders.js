import { useState, useEffect } from "react";
import orderService from "../services/orderService";

/**
 * Hook that handles getting all orders
 *
 * @returns orders, isLoading, error
 *
 * @category Order
 * @subcategory Hooks
 */
const useGetAllOrders = () => {
    // Orders that were fetched
    const [orders, setOrders] = useState([]);
    // Is request being processed
    const [isLoading, setIsLoading] = useState(false);
    // Error that occurred
    const [error, setError] = useState(null);

    useEffect(() => {
        // Process has started
        setIsLoading(true);
        // Get orders
        orderService
            .getAllOrders()
            .then((orders) => {
                // Set orderes that were fetched
                setOrders(orders);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => {
                setError(error); // Catch error and set error msg
            })
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, []);
    return { orders, isLoading, error };
};

export default useGetAllOrders;
