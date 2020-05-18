import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles getting all states
 *
 * @returns states, error, isLoading
 *
 * @category Item
 * @subcategory Hooks
 */
const useGetAllStates = () => {
    // states that were fetched
    const [states, setStates] = React.useState({});
    // Is request being processed
    const [error, setError] = React.useState(null);
    // Error that occurred
    const [isLoading, setIsLoading] = React.useState(false);

    React.useEffect(() => {
        // Process has started
        setIsLoading(true);
        // Get states
        itemService
            .getAllStates()
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
    }, []);
    return { states, error, isLoading };
};

export default useGetAllStates;
