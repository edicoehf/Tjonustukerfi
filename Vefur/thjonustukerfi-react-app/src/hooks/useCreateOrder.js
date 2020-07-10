import React from "react";
import orderService from "../services/orderService";

/**
 * Hook that handles creating an order
 *
 * @param initCb - Callback function that is called on success
 * @returns error, handleCreate, isProcessing, orderId
 *
 * @category Order
 * @subcategory Hooks
 */
const useCreateOrder = (initCb) => {
    // Error that occured
    const [error, setError] = React.useState(null);
    // Is the request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // The order to be created
    const [order, setOrder] = React.useState(null);
    // The ID of the order that was created
    const [orderId, setOrderId] = React.useState(null);
    // The callback function to be called on success
    const [cb, setCb] = React.useState(null);

    React.useEffect(() => {
        // If the a order has been set and request is not being processed, then create
        if (order && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Reset order id
            setOrderId(null);
            // Create order
            orderService
                .createOrder(order)
                .then((data) => {

                    console.log("Order created:");
                    console.log(data);

                    // Success, so set the new order ID for export
                    setOrderId(data.id);
                    //console.log(orderId);
                    // Set error as null in case it was earlier set due to error
                    setError(null);

                    // If a callback function was provided as an argument in the handleCreate function
                    // its called now
                    if (cb) {
                        cb();
                    }

                    // If callback function was provided in initialization its called now
                    if (initCb) {
                        initCb(data);
                    }
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setOrder(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, order, cb, initCb]);

    // Function that is exported for creating an order
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
