import React from "react";
import itemService from "../services/itemService";

const useGetItemLocations = () => {
    const [itemLocations, setItemLocations] = React.useState([]);
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);

    const fetchItemLocations = React.useCallback(() => {
        itemService
            .getItemLocations()
            .then((locations) => {
                setItemLocations(locations);
                setIsLoading(false);
                setError(null);
            })
            .catch((error) => setError(error));
    }, []);

    React.useEffect(() => {
        fetchItemLocations();
    }, [fetchItemLocations]);

    return { itemLocations, error, fetchItemLocations, isLoading };
};

export default useGetItemLocations;
