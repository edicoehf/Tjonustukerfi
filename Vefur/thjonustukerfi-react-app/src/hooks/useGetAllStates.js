import React from "react";
import itemService from "../services/itemService";

const useGetAllStates = () => {
    const [states, setStates] = React.useState({});
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(false);

    React.useEffect(() => {
        setIsLoading(true);
        itemService
            .getAllStates()
            .then((states) => {
                setStates(states);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);
    return { states, error, isLoading };
};

export default useGetAllStates;
