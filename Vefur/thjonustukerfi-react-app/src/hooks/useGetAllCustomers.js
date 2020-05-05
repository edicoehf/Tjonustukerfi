import { useState, useEffect } from "react";
import customerService from "../services/customerService";

const useGetAllCustomers = () => {
    const [customers, setCustomers] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        customerService
            .getAllCustomers()
            .then((customers) => {
                setCustomers(customers);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);
    return { customers, error, isLoading };
};

export default useGetAllCustomers;
