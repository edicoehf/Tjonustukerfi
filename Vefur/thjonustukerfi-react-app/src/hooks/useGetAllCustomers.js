import { useState, useEffect } from "react";
import customerService from "../services/customerService";

/**
 * Hook that handles getting all customers
 *
 * @returns customers, error, isLoading
 *
 * @category Customer
 * @subcategory Hooks
 */
const useGetAllCustomers = () => {
    // Customers that were fetched
    const [customers, setCustomers] = useState([]);
    // Is request being processed
    const [isLoading, setIsLoading] = useState(false);
    // Error that occurred
    const [error, setError] = useState(null);

    useEffect(() => {
        // Process has started
        setIsLoading(true);
        // Get customers
        customerService
            .getAllCustomers()
            .then((customers) => {
                // Set customeres that were fetched

                customers.sort((a, b) => a.name.localeCompare(b.name));
                
                setCustomers(customers);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, []);
    return { customers, error, isLoading };
};

export default useGetAllCustomers;
