import React from "react";

const useFilterOrders = (orders, initKey) => {
    const [filtered, setFiltered] = React.useState(orders);
    const [shouldFilter, setShouldFilter] = React.useState(true);

    const filterToggle = () => {
        setShouldFilter((prev) => !prev);
    };

    React.useEffect(() => {
        if (shouldFilter) {
            setFiltered(orders.filter((order) => order.dateCompleted === null));
        } else {
            setFiltered(orders);
        }
    }, [shouldFilter, orders]);

    return { filtered, filterToggle, shouldFilter };
};
export default useFilterOrders;
