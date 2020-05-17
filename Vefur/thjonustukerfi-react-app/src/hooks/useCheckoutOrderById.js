import React from "react";
import orderService from "../services/orderService";

/**
 * Hook that handles checking out an order
 *
 * @param id - Order ID
 * @param cb - Callback function thats called at end
 * @returns error, handleCheckout, isCheckingOut
 *
 * @category Order
 * @subcategory Hooks
 */
const useCheckoutOrderById = (id, cb) => {
    // Error that occured
    const [error, setError] = React.useState(null);
    // Is the request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Should the order be checked out
    const [isCheckingOut, setCheckingOut] = React.useState(false);

    React.useEffect(() => {
        // If the order should be checkout out and a request is not being processed, then check out
        if (isCheckingOut && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Check out
            orderService
                .checkoutOrderById(id)
                .then(() => {
                    // Success so set error as null incase it was earlier set due to error
                    setError(null);
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setCheckingOut(false);
                    setProcessing(false);
                    // If a callback function was provided then its called now
                    if (cb) {
                        cb();
                    }
                });
        }
    }, [id, isCheckingOut, isProcessing, cb]);

    // Function that is exported for checking out
    const handleCheckout = () => {
        if (!isCheckingOut) {
            setCheckingOut(true);
        }
    };

    return { error, handleCheckout, isCheckingOut };
};

export default useCheckoutOrderById;
