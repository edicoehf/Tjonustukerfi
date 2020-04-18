import React from "react";
import itemService from "../services/itemService";

const useGetItemById = (id) => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        itemService
            .getItemById(id)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);
    return { item, error };
};

export default useGetItemById;
