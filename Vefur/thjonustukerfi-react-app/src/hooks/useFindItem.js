import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles fetching an item by either id or barcode
 *
 * @returns items, error, fetchItemById, fetchItemByBarcode, isLoading
 * @category Item
 * @subcategory Hooks
 */
const useFindItem = () => {
    // Item that was fetched
    const [item, setItem] = React.useState({});
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(false);

    // Function that is exported for fetching an item by ID
    const fetchItemById = React.useCallback((id) => {
        // Process has started
        setIsLoading(true);
        // Get item
        itemService
            .getItemById(id)
            .then((item) => {
                // Set error as null incase it was earlier set due to error
                setError(null);
                // Set item that was fetched
                setItem(item);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, []);

    // Function that is exported for fetching an item by Barcode
    const fetchItemByBarcode = React.useCallback((barcode) => {
        // Process has started
        setIsLoading(true);
        // Get item
        itemService
            .getItemByBarcode(barcode)
            .then((item) => {
                // Set error as null incase it was earlier set due to error
                setError(null);
                // Set item that was fetched
                setItem(item);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, []);

    return { item, error, fetchItemById, fetchItemByBarcode, isLoading };
};

export default useFindItem;
