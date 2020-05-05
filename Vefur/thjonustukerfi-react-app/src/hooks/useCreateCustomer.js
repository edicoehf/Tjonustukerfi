import React from "react";
import customerService from "../services/customerService";

const useCreateCustomer = (initCb) => {
    const [error, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [customer, setCustomer] = React.useState(null);
    const [customerId, setCustomerId] = React.useState(null);

    React.useEffect(() => {
        if (customer && !isProcessing) {
            setProcessing(true);
            setCustomerId(null);
            customerService
                .createCustomer(customer)
                .then((data) => {
                    setCustomerId(data.customerId);
                    setError(null);

                    if (initCb) {
                        initCb();
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setCustomer(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, customer, initCb]);

    const handleCreate = (customer) => {
        if (!isProcessing) {
            setCustomer(customer);
        }
    };

    return { error, handleCreate, isProcessing, customerId };
};

export default useCreateCustomer;
