import React from "react";
import itemService from "../services/itemService";

const useItemPrintDetails = (id) => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);

    React.useEffect(() => {
        itemService
            .getItemPrintDetails(id)
            .then((item) => {
                item.json = JSON.parse(item.json);
                setItem(item);
                setIsLoading(false);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [id]);

    return { item, error, isLoading };
};

export default useItemPrintDetails;
