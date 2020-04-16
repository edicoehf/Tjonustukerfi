import React from "react";
import customerService from "../services/customerService";
import useGetAllOrders from "./useGetAllOrders";

const useDeleteCustomerById = (id) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isDeleting, setDeleting] = React.useState(false);
    const [isForceDeleting, setIsForceDeleting] = React.useState(false);
    const [modalIsOpen, setModalIsOpen] = React.useState(false);
    const { orders } = useGetAllOrders();

    React.useEffect(() => {
        if (isDeleting && !isProcessing) {
            setProcessing(true);
            customerService
                .deleteCustomerById(id)
                .then(() => {
                    setError(null);
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
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setIsForceDeleting(false);
                    setProcessing(false);
                });
        }
    }, [id, isDeleting, isProcessing, isForceDeleting]);

    const handleDelete = () => {
        var hasOrders = false;
        if (!error) {
            orders.forEach((order) => {
                if (order.customerId === Number(id)) {
                    setModalIsOpen(true);
                    hasOrders = true;
                }
            });
            if (!hasOrders) {
                setDeleting(true);
            }
        }
    };

    const handleForceDelete = () => {
        setIsForceDeleting(true);
        setModalIsOpen(false);
    };

    const handleClose = () => {
        setModalIsOpen(false);
    };

    return {
        error,
        handleDelete,
        isDeleting,
        modalIsOpen,
        handleClose,
        handleForceDelete,
    };
};

export default useDeleteCustomerById;
