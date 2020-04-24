import React from "react";
import itemService from "../services/itemService";

const useDeleteItemById = (id, cb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isDeleting, setDeleting] = React.useState(false);

    React.useEffect(() => {
        if (isDeleting && !isProcessing) {
            setProcessing(true);
            itemService
                .deleteItemById(id)
                .then(() => {
                    setError(null);
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setDeleting(false);
                    setProcessing(false);
                    if (cb) {
                        cb();
                    }
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

export default useDeleteItemById;
