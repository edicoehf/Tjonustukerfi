import React from "react";
import customerService from "../services/customerService";

const useDeleteCustomerById = id => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isDeleting, setDeleting] = React.useState(false);

    React.useEffect(() => {
        if (isDeleting && !isProcessing) {
            setProcessing(true);
            customerService
                .deleteCustomerById(id)
                .then(() => {
                    setError(null);
                })
                .catch(error => setError(error))
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

export default useDeleteCustomerById;
