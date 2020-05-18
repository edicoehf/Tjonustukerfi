import React from "react";
import customerService from "../services/customerService";
import useGetAllOrders from "./useGetAllOrders";

/**
 * Hook that handles deleting a customer, and the confirmation dialogs around it
 *
 * @param id - Customer ID
 * @param cb - Callback function that is called on success
 *
 * @category Customer
 * @subcategory Hooks
 */
const useDeleteCustomerById = (id, cb) => {
    // Error that occured
    const [error, setError] = React.useState(null);
    // Is the request being processed
    const [isProcessing, setProcessing] = React.useState(false);
    // Should the customer be deleted
    const [isDeleting, setDeleting] = React.useState(false);
    // Should the customer be force deleted (even if it has active orders)
    const [isForceDeleting, setIsForceDeleting] = React.useState(false);
    // Is confirmation dialog for force delete open
    const [forceModalOpen, setForceModalOpen] = React.useState(false);
    // Is confirmation dialog for delete open
    const [softModalOpen, setSoftModalOpen] = React.useState(false);
    // Get all orders, to see if customer has active orders
    const { orders } = useGetAllOrders();

    React.useEffect(() => {
        // If the customer should be deleted and a request is not being processed, then delete
        if (isDeleting && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Delete customer
            customerService
                .deleteCustomerById(id)
                .then(() => {
                    // Success so set error as null incase it was earlier set due to error
                    setError(null);
                    // If a callback function was provided then its called now
                    if (cb !== undefined) {
                        cb();
                    }
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setDeleting(false);
                    setProcessing(false);
                });
        }
        // If the customer should be force deleted and a request is not being processed, then delete
        if (isForceDeleting && !isProcessing) {
            // Process has started
            setProcessing(true);
            // Force delete customer
            customerService
                .forceDeleteCustomerById(id)
                .then(() => {
                    // Success so set error as null incase it was earlier set due to error
                    setError(null);
                    // If a callback function was provided then its called now
                    if (cb !== undefined) {
                        cb();
                    }
                })
                .catch((error) => setError(error)) // Catch error and set error msg
                .finally(() => {
                    // Process has finished, successful or not
                    setIsForceDeleting(false);
                    setProcessing(false);
                });
        }
    }, [id, isDeleting, isProcessing, isForceDeleting, cb]);

    // Function that is exported for delete, opens the appropriate modal
    const handleDelete = () => {
        if (!error) {
            if (orders.some((order) => order.customerId === Number(id))) {
                setForceModalOpen(true);
            } else {
                setSoftModalOpen(true);
            }
        }
    };

    // Function that is exported for force delete
    const handleForceDelete = () => {
        setForceModalOpen(false);
        setIsForceDeleting(true);
    };

    // Function that is exported for delete
    const handleSoftDelete = () => {
        setSoftModalOpen(false);
        setDeleting(true);
    };

    // Close confirmation dialogs
    const handleClose = () => {
        setSoftModalOpen(false);
        setForceModalOpen(false);
    };

    return {
        error,
        handleDelete,
        isDeleting,
        forceModalOpen,
        softModalOpen,
        handleClose,
        handleForceDelete,
        handleSoftDelete,
        isForceDeleting,
    };
};

export default useDeleteCustomerById;
