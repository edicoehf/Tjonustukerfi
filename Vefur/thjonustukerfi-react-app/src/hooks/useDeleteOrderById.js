import React from "react";
import orderService from "../services/orderService";

const useDeleteOrderById = (id, cb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isDeleting, setDeleting] = React.useState(false);

    React.useEffect(() => {
        if (isDeleting && !isProcessing) {
            setProcessing(true);
            orderService
                .deleteOrderById(id)
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
    }, [id, isDeleting, isProcessing, cb]);

    const handleDelete = () => {
        if (!isDeleting) {
            setDeleting(true);
        }
    };

    return { error, handleDelete, isDeleting };
};

export default useDeleteOrderById;
