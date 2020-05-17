import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles getting item locations
 *
 * @returns itemLocations, errr, fetchItemLocations, isLoading
 *
 * @category Item
 * @subcategory Hooks
 */
const useGetItemLocations = () => {
    // Locations that were fetched
    const [itemLocations, setItemLocations] = React.useState([]);
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(true);

    // Export the fetch also, so it can be refetched on demand
    const fetchItemLocations = React.useCallback(() => {
        // Processing started
        setIsLoading(true);
        // Get item
        itemService
            .getItemLocations()
            .then((locations) => {
                // Set locations that were fetced
                setItemLocations(locations);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, []);

    // Fetch is still done in the useEffect as well
    React.useEffect(() => {
        fetchItemLocations();
    }, [fetchItemLocations]);

    return { itemLocations, error, fetchItemLocations, isLoading };
};

export default useGetItemLocations;
