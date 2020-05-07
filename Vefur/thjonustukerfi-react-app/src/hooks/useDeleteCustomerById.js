import React from "react";
import customerService from "../services/customerService";
import useGetAllOrders from "./useGetAllOrders";

const useDeleteCustomerById = (id, cb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isDeleting, setDeleting] = React.useState(false);
    const [isForceDeleting, setIsForceDeleting] = React.useState(false);
    const [forceModalOpen, setForceModalOpen] = React.useState(false);
    const [softModalOpen, setSoftModalOpen] = React.useState(false);
    const { orders } = useGetAllOrders();

    React.useEffect(() => {
        if (isDeleting && !isProcessing) {
            setProcessing(true);
            customerService
                .deleteCustomerById(id)
                .then(() => {
                    setError(null);
                    if (cb !== undefined) {
                        cb();
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setDeleting(false);
                    setProcessing(false);
                });
        }
        if (isForceDeleting && !isProcessing) {
            setProcessing(true);
            customerService
                .forceDeleteCustomerById(id)
                .then(() => {
                    setError(null);
                    if (cb !== undefined) {
                        cb();
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setIsForceDeleting(false);
                    setProcessing(false);
                });
        }
    }, [id, isDeleting, isProcessing, isForceDeleting, cb]);

    const handleDelete = () => {
        if (!error) {
            if (orders.some((order) => order.customerId === Number(id))) {
                setForceModalOpen(true);
            } else {
                setSoftModalOpen(true);
            }
        }
    };

    const handleForceDelete = () => {
        setForceModalOpen(false);
        setIsForceDeleting(true);
    };

    const handleSoftDelete = () => {
        setSoftModalOpen(false);
        setDeleting(true);
    };

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
