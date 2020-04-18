import React from "react";
import itemService from "../services/itemService";

const useGetNextStatesById = (id) => {
    const [states, setStates] = React.useState({});
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        itemService
            .getNextStatesById(id)
            .then((states) => {
                setStates(states);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);
    return { states, error };
};

export default useGetNextStatesById;
