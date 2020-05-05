import React from "react";
import orderService from "../services/orderService";

const useCreateOrder = (initCb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [order, setOrder] = React.useState(null);
    const [cb, setCb] = React.useState(null);
    const [orderId, setOrderId] = React.useState(null);

    React.useEffect(() => {
        if (order && !isProcessing) {
            setProcessing(true);
            setOrderId(null);
            orderService
                .createOrder(order)
                .then((data) => {
                    setOrderId(data.orderId);
                    setError(null);
                    if (cb) {
                        cb();
                    }
                    if (initCb) {
                        initCb();
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setOrder(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, order, cb, initCb]);

    const handleCreate = (order, paraCb) => {
        if (!isProcessing) {
            if (paraCb !== undefined) {
                setCb(() => paraCb);
            }
            setOrder(order);
        }
    };

    return { error, handleCreate, isProcessing, orderId };
};

export default useCreateOrder;
