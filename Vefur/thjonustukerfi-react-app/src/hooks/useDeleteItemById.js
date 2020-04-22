import React from "react";
import itemService from "../services/itemService";

const useDeleteItemById = (id) => {
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
                });
        }
    }, [id, isDeleting, isProcessing]);

    const handleDelete = () => {
        if (!isDeleting) {
            setDeleting(true);
        }
    };

    return { error, handleDelete, isDeleting };
};

export default useDeleteItemById;
