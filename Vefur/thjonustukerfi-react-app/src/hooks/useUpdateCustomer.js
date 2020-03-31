import React from "react";
import customerService from "../services/customerService";

const useUpdateCustomer = customer => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [isUpdating, setUpdating] = React.useState(false);

    React.useEffect(() => {
        if (isUpdating && !isProcessing) {
            setProcessing(true);
            customerService
                .updateCustomer(customer)
                .then(() => {
                    setError(null);
                })
                .catch(error => setError(error))
                .finally(() => {
                    setUpdating(false);
                    setProcessing(false);
                });
        }
    }, [id, isUpdating, isProcessing]);

    const handleUpdate = () => {
        if (!isUpdating) {
            setUpdating(true);
        }
    };

    return { error, handleUpdate, isUpdating };
};

export default useUpdateCustomer;
