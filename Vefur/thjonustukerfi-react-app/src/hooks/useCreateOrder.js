import React from "react";
import orderService from "../services/orderService";

const useCreateOrder = (initCb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [order, setOrder] = React.useState(null);
    const [cb, setCb] = React.useState(initCb);

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
                    if (cb) {
                        cb();
                    }
                });
        }
    }, [isProcessing, order, cb]);

    const handleCreate = (order, paraCb) => {
        if (paraCb) {
            setCb(paraCb);
        }
        if (!isProcessing) {
            setOrder(order);
        }
    };

    return { error, handleCreate, isProcessing };
};

export default useCreateOrder;
