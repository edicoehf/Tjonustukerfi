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
            .getAllRawOrders()
            .then((orders) => {
                // Set orders that were fetched:
                //console.log("Orders have been fetched and are:");
                //console.log(orders)
                orders = orders.sort((a,b) => -a.dateCreated.localeCompare(b.dateCreated));
                //console.log("Orders after sorting:");
                //console.log(orders);
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
