import React from "react";
import orderService from "../services/orderService";

const initState = {
    id: null,
    customer: "",
    customerId: "",
    barcode: "",
    items: [],
    dateCreated: null,
    dateModified: null,
    dateCompleted: null,
};

/**
 * Hook that handles getting an order by ID
 *
 * @param id Order ID
 * @returns order, error, fetchOrder, isLoading
 *
 * @category Order
 * @subcategory Hooks
 */
const useGetOrderById = (id) => {
    // Order that was fetched
    const [order, setOrder] = React.useState(initState);
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(false);

    // Export the fetch also, so it can be refetched on demand
    const fetchOrder = React.useCallback(() => {
        // Process has started
        setIsLoading(true);
        // Get item
        orderService
            .getOrderById(id)
            .then((order) => {
                // Parse the extra json info
                order.items = parseItemJsonForItems(order.items);
                // Set item that was fetched
                setOrder(order);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, [id]);

    // Fetch is still done in the useEffect as well
    React.useEffect(() => {
        fetchOrder();
    }, [fetchOrder]);

    return { order, error, fetchOrder, isLoading };
};
const parseItemJsonForItems = (items) =>
    items.map((item) => {
        return { ...item, json: JSON.parse(item.json) };
    });

export default useGetOrderById;
