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
                order.items = parseItemJsonForItems(order.items);
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
const parseItemJsonForItems = (items) =>
    items.map((item) => {
        return { ...item, json: JSON.parse(item.json) };
    });

export default useGetOrderById;
