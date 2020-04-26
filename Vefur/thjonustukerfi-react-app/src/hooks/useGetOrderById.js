import React from "react";
import orderService from "../services/orderService";

const initState = {
    id: "",
    customer: "",
    customerId: "",
    barcode: "",
    items: [],
    dateCreated: null,
    dateModified: null,
    dateCompleted: null,
};

const useGetOrderById = (id) => {
    const [order, setOrder] = React.useState(initState);
    const [error, setError] = React.useState(null);

    const fetchOrder = React.useCallback(() => {
        orderService
            .getOrderById(id)
            .then((order) => {
                setOrder(order);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);

    React.useEffect(() => {
        fetchOrder();
    }, [fetchOrder]);

    return { order, error, fetchOrder };
};

export default useGetOrderById;
