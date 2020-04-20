import React from "react";
import itemService from "../services/itemService";

const useGetNextStatesById = (id) => {
    const [states, setStates] = React.useState({});
    const [error, setError] = React.useState(null);

    const fetchNextStates = React.useCallback(() => {
        itemService
            .getNextStatesById(id)
            .then((states) => {
                setStates(states);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);

    React.useEffect(() => {
        fetchNextStates();
    }, [fetchNextStates]);
    return { states, error, fetchNextStates };
};

export default useGetNextStatesById;
