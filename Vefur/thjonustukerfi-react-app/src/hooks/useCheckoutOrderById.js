import React from "react";
import orderService from "../services/orderService";

const useCheckoutOrderById = (id, cb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isCheckingOut, setCheckingOut] = React.useState(false);

    React.useEffect(() => {
        if (isCheckingOut && !isProcessing) {
            setProcessing(true);
            orderService
                .checkoutOrderById(id)
                .then(() => {
                    setError(null);
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setCheckingOut(false);
                    setProcessing(false);
                    if (cb) {
                        cb();
                    }
                });
        }
    }, [id, isCheckingOut, isProcessing, cb]);

    const handleCheckout = () => {
        if (!isCheckingOut) {
            setCheckingOut(true);
        }
    };

    return { error, handleCheckout, isCheckingOut };
};

export default useCheckoutOrderById;
