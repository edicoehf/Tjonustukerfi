import React from "react";
import itemService from "../services/itemService";

const useGetNextStatesById = (id) => {
    const [states, setStates] = React.useState({});
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(false);

    const fetchNextStates = React.useCallback(() => {
        setIsLoading(true);
        itemService
            .getNextStatesById(id)
            .then((states) => {
                setStates(states);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, [id]);

    React.useEffect(() => {
        fetchNextStates();
    }, [fetchNextStates]);
    return { states, error, fetchNextStates, isLoading };
};

export default useGetNextStatesById;
