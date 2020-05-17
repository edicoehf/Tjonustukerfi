import React from "react";
import orderService from "../services/orderService";

/**
 * Hook that handles updating an item
 *
 * @param id - Item ID
 * @returns updateError, handleUpdate, isProcessing, hasUpdated
 *
 * @category Item
 * @subcategory Hooks
 */
const useUpdateOrder = (id) => {
    // Error that occurred
    const [updateError, setError] = React.useState(null);
    // Is request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Item values to update
    const [values, setValues] = React.useState(null);
    // Has the order been updated
    const [hasUpdated, setHasUpdated] = React.useState(false);

    React.useEffect(() => {
        // If values have been set and no current process, then update
        if (values && !isProcessing) {
            // Process started
            setProcessing(true);
            // Update item
            orderService
                .updateOrderById(values, id)
                .then(() => {
                    // Set error as null incase it was earlier set due to error
                    setError(null);
                    // Set updated was success
                    setHasUpdated(true);
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setValues(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, values, id]);

    // Exported function that triggers the update
    const handleUpdate = (values) => {
        if (!isProcessing) {
            setHasUpdated(false);
            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing, hasUpdated };
};

export default useUpdateOrder;
