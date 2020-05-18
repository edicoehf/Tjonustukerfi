import { useState, useEffect } from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles getting all orders related to a customer by ID
 *
 * @param id - Customer ID
 * @returns orders, isLoading, error
 *
 * @category Order
 * @subcategory Hooks
 */
const useGetOrdersByCustomerId = (id) => {
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
        customerService
            .getOrdersByCustomerId(id)
            .then((orders) => {
                // Set orders that was fetched
                setOrders(orders);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, [id]);
    return { orders, isLoading, error };
};

export default useGetOrdersByCustomerId;
