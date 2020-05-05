import React from "react";
import serviceService from "../services/serviceService";

const useGetServices = () => {
    const [services, setServices] = React.useState([]);
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(false);

    React.useEffect(() => {
        setIsLoading(true);
        serviceService
            .getServices()
            .then((services) => {
                setServices(services);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);
    return { services, error, isLoading };
};

export default useGetServices;
