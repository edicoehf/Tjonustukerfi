import React from "react";
import orderService from "../services/orderService";

const useGetOrderById = id => {
    const [order, setOrder] = React.useState({});
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        orderService
            .getOrderById(id)
            .then(order => {
                setOrder(order);
                setError(null);
            })
            .catch(error => setError(error));
    }, [id]);
    return { order, error };
};

export default useGetOrderById;
