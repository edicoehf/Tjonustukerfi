import React from "react";
import serviceService from "../services/serviceService";

const useGetServices = () => {
    const [services, setServices] = React.useState({});
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        serviceService
            .getServices()
            .then(services => {
                setServices(services);
                setError(null);
            })
            .catch(error => setError(error));
    }, []);
    return { services, error };
};

export default useGetServices;
