import React from "react";
import customerService from "../services/customerService";

const useGetCustomerById = (id) => {
    const [customer, setCustomer] = React.useState({});
    const [error, setError] = React.useState(null);
    const [isProcessing, setIsProcessing] = React.useState(false);

    React.useEffect(() => {
        setIsProcessing(true);
        customerService
            .getCustomerById(id)
            .then((customer) => {
                setCustomer(customer);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsProcessing(false);
            });
    }, [id]);
    return { customer, error, isProcessing };
};

export default useGetCustomerById;
