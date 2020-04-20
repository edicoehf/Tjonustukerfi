import React from "react";
import itemService from "../services/itemService";

const useGetItemById = (id) => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);

    const fetchItem = React.useCallback(() => {
        itemService
            .getItemById(id)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);

    React.useEffect(() => {
        fetchItem();
    }, [fetchItem]);

    return { item, error, fetchItem };
};

export default useGetItemById;
