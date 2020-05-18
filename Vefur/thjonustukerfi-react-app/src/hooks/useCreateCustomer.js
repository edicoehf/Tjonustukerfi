import React from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles creating a customer
 *
 * @param cb - Callback function that's called on success
 * @returns error, handleCreate, isProcessing, customerId
 *
 * @category Customer
 * @subcategory Hooks
 */
const useCreateCustomer = (cb) => {
    // Error that occured
    const [error, setError] = React.useState(null);
    // Is the request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // The customer to be created
    const [customer, setCustomer] = React.useState(null);
    // The ID of the customer that was created
    const [customerId, setCustomerId] = React.useState(null);

    React.useEffect(() => {
        // If the a customer has been set request is not being processed, then create
        if (customer && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Reset customer id
            setCustomerId(null);
            // Create customer
            customerService
                .createCustomer(customer)
                .then((data) => {
                    // Success, so set the new customers ID for export
                    setCustomerId(data.customerId);
                    // Set error as null incase it was earlier set due to error
                    setError(null);

                    // If callback function was provided its called now
                    if (cb) {
                        cb();
                    }
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setCustomer(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, customer, cb]);

    // Function that is exported for creating a customer
    const handleCreate = (customer) => {
        if (!isProcessing) {
            setCustomer(customer);
        }
    };

    return { error, handleCreate, isProcessing, customerId };
};

export default useCreateCustomer;
