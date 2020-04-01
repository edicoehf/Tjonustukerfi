import React, { useContext } from "react";
import customerService from "../services/customerService";
import { CustomerContext } from "../context/customerContext";

const useGetCustomerById = id => {
    const { customer, setCustomer } = useContext(CustomerContext);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        customerService
            .getCustomerById(id)
            .then(customer => {
                setCustomer(customer);
                setError(null);
            })
            .catch(error => setError(error));
    }, [id, setCustomer]);
    return { customer, error };
};

export default useGetCustomerById;
