import React from "react";
import orderService from "../services/orderService";

const useCreateOrder = () => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [order, setOrder] = React.useState(null);

    React.useEffect(() => {
        if (order && !isProcessing) {
            setProcessing(true);
            orderService
                .createOrder(order)
                .then(() => {
                    setError(null);
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setOrder(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, order]);

    const handleCreate = (order) => {
        if (!isProcessing) {
            setOrder(order);
        }
    };

    return { error, handleCreate, isProcessing };
};

export default useCreateOrder;
