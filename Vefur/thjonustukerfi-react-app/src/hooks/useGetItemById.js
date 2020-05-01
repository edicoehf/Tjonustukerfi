import React from "react";
import itemService from "../services/itemService";

const useGetItemById = (id) => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);

    const fetchItem = React.useCallback(() => {
        itemService
            .getItemById(id)
            .then((item) => {
                item.json = JSON.parse(item.json);
                setItem(item);
                setIsLoading(false);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);

    React.useEffect(() => {
        fetchItem();
    }, [fetchItem]);

    return { item, error, fetchItem, isLoading };
};

export default useGetItemById;
