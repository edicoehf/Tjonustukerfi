import React from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles getting the customer with the given ID
 *
 * @param id - Customer ID
 * @returns customer, error, isProcessing
 *
 * @category Customer
 * @subcategory Hooks
 */
const useGetCustomerById = (id) => {
    // Customer that was fetched
    const [customer, setCustomer] = React.useState({});
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isProcessing, setIsProcessing] = React.useState(false);

    React.useEffect(() => {
        // Process has started
        setIsProcessing(true);
        customerService
            // Get customer
            .getCustomerById(id)
            .then((customer) => {
                // Set customer that was fetched
                setCustomer(customer);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsProcessing(false);
            });
    }, [id]);
    return { customer, error, isProcessing };
};

export default useGetCustomerById;
