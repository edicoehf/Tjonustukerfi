import React from "react";
import itemService from "../services/itemService";

const useGetItemHistoryById = (id) => {
    const [itemHistory, setItemHistory] = React.useState([]);
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);

    const fetchItemHistory = React.useCallback(() => {
        itemService
            .getItemHistoryById(id)
            .then((history) => {
                setItemHistory(history);
                setIsLoading(false);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);

    React.useEffect(() => {
        fetchItemHistory();
    }, [fetchItemHistory]);

    return { itemHistory, error, fetchItemHistory, isLoading };
};

export default useGetItemHistoryById;
