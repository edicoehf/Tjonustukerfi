import React from "react";
import customerService from "../services/customerService";

const useGetAllCustomers = () => {
    const [customers, setCustomers] = React.useState([]);
    const [isLoading, setIsLoading] = React.useState(true);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        customerService
            .getAllCustomers()
            .then(customers => {
                setCustomers(customers);
                setIsLoading(false);
                setError(null);
            })
            .catch(error => setError(error));
    }, []);
    return { customers, error, isLoading };
};

export default useGetAllCustomers;
