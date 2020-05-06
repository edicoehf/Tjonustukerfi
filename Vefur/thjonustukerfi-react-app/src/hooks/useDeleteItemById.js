import React from "react";
import itemService from "../services/itemService";
import useGetItemById from "./useGetItemById";

const useDeleteItemById = (id, cb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isDeleting, setDeleting] = React.useState(false);
    const { item } = useGetItemById(id);

    React.useEffect(() => {
        if (isDeleting && !isProcessing) {
            setProcessing(true);
            itemService
                .deleteItemById(id)
                .then(() => {
                    setError(null);
                    if (cb) {
                        cb(item.orderId);
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setDeleting(false);
                    setProcessing(false);
                });
        }
    }, [id, isDeleting, isProcessing, cb, item]);

    const handleDelete = () => {
        if (!isDeleting) {
            setDeleting(true);
        }
    };

    return { error, handleDelete, isDeleting };
};

export default useDeleteItemById;
