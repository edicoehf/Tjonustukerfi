import React from "react";

/**
 * Hook that handles filtering a list of order by whether they have been picked up
 *
 * @param orders - List of orders
 * @returns filtered, filterToggle, shouldFilter
 *
 * @category Orders
 * @subcategory Hooks
 */
const useFilterOrders = (orders) => {
    // The orders to be filtered
    const [filtered, setFiltered] = React.useState(orders);
    // Should the orders be filtered by pickup
    const [shouldFilter, setShouldFilter] = React.useState(true);

    // Function that is export for toggling filter on/off
    const filterToggle = () => {
        setShouldFilter((prev) => !prev);
    };

    React.useEffect(() => {
        // Filter orders if toggled on, else use original list
        if (shouldFilter) {
            setFiltered(orders.filter((order) => order.dateCompleted === null));
        } else {
            setFiltered(orders);
        }
    }, [shouldFilter, orders]);

    return { filtered, filterToggle, shouldFilter };
};
export default useFilterOrders;
