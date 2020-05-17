import React from "react";
import orderService from "../services/orderService";

/**
 * Hook that handles deleting an order
 *
 * @param id - Order ID
 * @param cb - Callback function that's called on success
 * @returns error, handleDelete, isDeleting
 *
 * @category Order
 * @subcategory Hooks
 */
const useDeleteOrderById = (id, cb) => {
    // Error that occured
    const [error, setError] = React.useState(null);
    // Is the request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Should order be deleted
    const [isDeleting, setDeleting] = React.useState(false);

    React.useEffect(() => {
        // If order should be deleted and request is not being processed, then delete
        if (isDeleting && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Delete order
            orderService
                .deleteOrderById(id)
                .then(() => {
                    // Set error as null incase it was earlier set due to error
                    setError(null);

                    // If callback function was provided its called now
                    if (cb !== undefined) {
                        cb();
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    // Process has finished, successful or not
                    setDeleting(false);
                    setProcessing(false);
                });
        }
    }, [id, isDeleting, isProcessing, cb]);

    // Function that is exported for deleting order
    const handleDelete = () => {
        if (!isDeleting) {
            setDeleting(true);
        }
    };

    return { error, handleDelete, isDeleting };
};

export default useDeleteOrderById;
