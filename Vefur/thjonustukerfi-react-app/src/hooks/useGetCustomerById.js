import React from "react";
import customerService from "../services/customerService";
import {
    CustomerContext,
    CustomerErrorContext
} from "../context/customerContext";

const useGetCustomerById = id => {
    const [customer, setCustomer] = React.useState({});
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        customerService
            .getCustomerById(id)
            .then(customer => {
                setCustomer(customer);
                setError(null);
            })
            .catch(error => setError(error));
    }, [id]);
    return { customer, error };
};

export default useGetCustomerById;
