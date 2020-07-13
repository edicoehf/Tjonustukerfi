import React from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles updating a customer email
 *
 * @param cb - Callback function that's called on success
 * @returns updateError, handleUpdate, isProcessing
 *
 * @category Customer
 * @subcategory Hooks
 */
const useUpdateCustomerEmail = (cb) => {
    // Error that occurred
    const [updateError, setError] = React.useState(null);
    // Is request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Customer values to update
    const [emailObj, setEmail] = React.useState(null);

    React.useEffect(() => {
        // If values have been set and no current process, then update
        if (emailObj && !isProcessing) {
            // Process started
            setProcessing(true);
            const id = emailObj.id;
            const email = {
                email: emailObj.email
            };
            customerService
                .updateCustomerEmail(email, id)
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
                    setEmail(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, emailObj, cb]);

    // Exported function that triggers the update
    const handleUpdateEmail = (emailObj) => {
        if (!isProcessing) {
            setEmail(emailObj);
        }
    };

    return { updateError, handleUpdateEmail, isProcessing };
};

export default useUpdateCustomerEmail;
