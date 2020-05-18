import React from "react";
import itemService from "../services/itemService";
import useGetItemById from "./useGetItemById";

/**
 * Hook that handles deleting an item
 *
 * @param id - Item ID
 * @param cb - Callback function thats called on success
 * @returns error, handleDelete, isDeleting
 *
 * @category Item
 * @subcategory Hooks
 */
const useDeleteItemById = (id, cb) => {
    // Error that occurres
    const [error, setError] = React.useState(null);
    // Is the request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Should item be deleted
    const [isDeleting, setDeleting] = React.useState(false);
    // Get item so its order id can be sent to the cb
    const { item } = useGetItemById(id);

    React.useEffect(() => {
        // If item should be deleted and request is not being processed, then delete
        if (isDeleting && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Delete item
            itemService
                .deleteItemById(id)
                .then(() => {
                    // Set error as null incase it was earlier set due to error
                    setError(null);

                    // If callback function was provided its called now
                    if (cb) {
                        cb(item.orderId);
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    // Process has finished, successful or not
                    setDeleting(false);
                    setProcessing(false);
                });
        }
    }, [id, isDeleting, isProcessing, cb, item]);

    // Function that is exported for deleting item
    const handleDelete = () => {
        if (!isDeleting) {
            setDeleting(true);
        }
    };

    return { error, handleDelete, isDeleting };
};

export default useDeleteItemById;
