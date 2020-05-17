import React from "react";
import categoryService from "../services/categoryService";

/**
 * Hook that handles getting categories
 *
 * @returns catgegories, error, isLoading
 *
 * @category Info
 * @subcategory Hooks
 */
const useGetCategories = () => {
    // categories that were fetched
    const [categories, setCategories] = React.useState([]);
    // Error that occurred
    const [error, setError] = React.useState(null);
    // Is request being processed
    const [isLoading, setIsLoading] = React.useState(false);

    React.useEffect(() => {
        // Process has started
        setIsLoading(true);
        // Get categories
        categoryService
            .getCategories()
            .then((categories) => {
                // Set categories that were fetched
                setCategories(categories);
                // Set error as null incase it was earlier set due to error
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                // Process has finished, successful or not
                setIsLoading(false);
            });
    }, []);
    return { categories, error, isLoading };
};

export default useGetCategories;
