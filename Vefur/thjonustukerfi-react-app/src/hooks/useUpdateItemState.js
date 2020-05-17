import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles updating an items state
 *
 * @param cb - Callback function that's called on success
 * @returns updateError, handleUpdate, isProcessing
 *
 * @category Item
 * @subcategory Hooks
 */
const useUpdateItemState = (cb) => {
    // Error that occurred
    const [updateError, setError] = React.useState(null);
    // Is request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // State to update to
    const [values, setValues] = React.useState(null);

    React.useEffect(() => {
        // If state has been set and no current process, then update
        if (values && !isProcessing) {
            // Process started
            setProcessing(true);
            // Update state
            itemService
                .updateItemState(values)
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

export default useUpdateItemState;
