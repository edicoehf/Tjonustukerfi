import React from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles updating a customer
 *
 * @param cb - Callback function that's called on success
 * @returns updateError, handleUpdate, isProcessing
 *
 * @category Customer
 * @subcategory Hooks
 */
const useUpdateCustomer = (cb) => {
    // Error that occurred
    const [updateError, setError] = React.useState(null);
    // Is request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Customer values to update
    const [values, setValues] = React.useState(null);

    React.useEffect(() => {
        // If values have been set and no current process, then update
        if (values && !isProcessing) {
            // Process started
            setProcessing(true);
            // Update customer
            customerService
                .updateCustomer(values)
                .then(() => {
                    // Set error as null incase it was earlier set due to error
                    setError(null);
                    // If a cb was provided, it's called now
                    if (cb) {
                        cb();
                    }
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setValues(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, values, cb]);

    // Exported function that triggers the update
    const handleUpdate = (values) => {
        if (!isProcessing) {
            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing };
};

export default useUpdateCustomer;
