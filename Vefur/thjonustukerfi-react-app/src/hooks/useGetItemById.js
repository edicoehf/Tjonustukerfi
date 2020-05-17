import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles getting an item by ID
 *
 * @param id - Item ID
 * @returns item, error, fetchItem, isLoading
 *
 * @category Item
 * @subcategory Hooks
 */
const useGetItemById = (id) => {
    // Item that was fetched
    const [item, setItem] = React.useState({});
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(false);

    // Export the fetch also, so it can be refetched on demand
    const fetchItem = React.useCallback(() => {
        // Process has started
        setIsLoading(true);
        itemService
            // Get item
            .getItemById(id)
            .then((item) => {
                // Parse the extra json info
                item.json = JSON.parse(item.json);
                // Set item that was fetched
                setItem(item);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, [id]);

    // Fetch is still done in the useEffect as well
    React.useEffect(() => {
        fetchItem();
    }, [fetchItem]);

    return { item, error, fetchItem, isLoading };
};

export default useGetItemById;
