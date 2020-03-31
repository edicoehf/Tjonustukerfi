import React from "react";
import customerService from "../services/customerService";

const useUpdateCustomer = () => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [customer, setCustomer] = React.useState(null);

    React.useEffect(() => {
        if (customer && !isProcessing) {
            setProcessing(true);
            customerService
                .updateCustomer(customer)
                .then(() => {
                    setError(null);
                })
                .catch(error => setError(error))
                .finally(() => {
                    setCustomer(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, customer]);

    const handleUpdate = customer => {
        if (!isProcessing) {
            setCustomer(customer);
        }
    };

    return { error, handleUpdate, isProcessing };
};

export default useUpdateCustomer;
