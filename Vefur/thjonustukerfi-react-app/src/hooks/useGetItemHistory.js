import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles getting all previous states of an item
 *
 * @param id - Item ID
 * @returns itemHistory, error, fetchItemHistory, isLoading
 *
 * @category Item
 * @subcategory Hooks
 */
const useGetItemHistoryById = (id) => {
    // History that was fetched
    const [itemHistory, setItemHistory] = React.useState([]);
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(false);

    // Export the fetch also, so it can be refetched on demand
    const fetchItemHistory = React.useCallback(() => {
        // Process has started
        setIsLoading(true);
        // Fetch item
        itemService
            .getItemHistoryById(id)
            .then((history) => {
                // Set item that was fetched
                setItemHistory(history);
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
        fetchItemHistory();
    }, [fetchItemHistory]);

    return { itemHistory, error, fetchItemHistory, isLoading };
};

export default useGetItemHistoryById;
