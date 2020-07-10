import React from "react";
import itemService from "../services/itemService";

/**
 * Hook that handles getting print details for a certain item
 *
 * @param id - Item ID
 * @returns item, error, isLoading
 *
 * @category Item
 * @subcategory Hooks
 */
const useItemPrintDetails = (id) => {
    // Item details were fetched
    const [item, setItem] = React.useState({});
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(true);

    React.useEffect(() => {
        // Process has started
        // Get details
        itemService
            .getItemPrintDetails(id)
            .then((item) => {
                console.log("Print item has been fetched!")
                // Parse the extra json info
                item.json = JSON.parse(item.json);
                // Set details that was fetched
                setItem(item);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error)) // Catch error and set error msg
            .finally(() => {
                // Set details that were fetched
                setIsLoading(false);
            });
    }, [id]);

    return { item, error, isLoading };
};

export default useItemPrintDetails;
