import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles getting all the available next states for a given item
 *
 * @param id - Item ID
 * @returns states, error, fetchNextStates, isLoading
 *
 * @category Item
 * @subcategory Hooks
 */
const useGetNextStatesById = (id) => {
    // States that were fetched
    const [states, setStates] = React.useState({});
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(false);

    // Export the fetch also, so it can be refetched on demand
    const fetchNextStates = React.useCallback(() => {
        // Process has started
        setIsLoading(true);
        itemService
            // Get states
            .getNextStatesById(id)
            .then((states) => {
                // Set states that were fetched
                setStates(states);
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
        fetchNextStates();
    }, [fetchNextStates]);
    return { states, error, fetchNextStates, isLoading };
};

export default useGetNextStatesById;
