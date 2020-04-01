import React from "react";
import customerService from "../services/customerService";

const useCreateCustomer = () => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [customer, setCustomer] = React.useState(null);

    React.useEffect(() => {
        if (customer && !isProcessing) {
            setProcessing(true);
            customerService
                .createCustomer(customer)
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

    const handleCreate = customer => {
        if (!isProcessing) {
            setCustomer(customer);
        }
    };

    return { error, handleCreate, isProcessing };
};

export default useCreateCustomer;
