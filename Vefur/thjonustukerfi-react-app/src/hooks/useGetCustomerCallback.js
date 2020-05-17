import React from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles getting a customer by ID on CB call only
 *
 * @param id - Customer ID
 * @param cb - Callback function to be called on success
 * @returns customer, error, fetchcustomer
 *
 * @category Customer
 * @subcategory Hooks
 */
const useGetCustomerCallback = (id, cb) => {
    // Customer that was fetched
    const [customer, setCustomer] = React.useState({});
    // Error that occurred
    const [error, setError] = React.useState(null);

    const fetchCustomer = React.useCallback(() => {
        // Get customer
        customerService
            .getCustomerById(id)
            .then((customer) => {
                // Set customer that was fetched
                setCustomer(customer);
                // call CB with customer
                cb(customer);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id, cb]);

    return { customer, error, fetchCustomer };
};

export default useGetCustomerCallback;
