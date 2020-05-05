import React from "react";
import customerService from "../services/customerService";

const useGetCustomerCallback = (id, cb) => {
    const [customer, setCustomer] = React.useState({});
    const [error, setError] = React.useState(null);

    const fetchCustomer = React.useCallback(() => {
        customerService
            .getCustomerById(id)
            .then((customer) => {
                setCustomer(customer);
                cb(customer);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id, cb]);

    return { customer, error, fetchCustomer };
};

export default useGetCustomerCallback;
