import React from "react";
import itemService from "../services/itemService";

const useGetItemLocations = () => {
    const [itemLocations, setItemLocations] = React.useState([]);
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);

    const fetchItemLocations = React.useCallback(() => {
        setIsLoading(true);
        itemService
            .getItemLocations()
            .then((locations) => {
                setItemLocations(locations);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    React.useEffect(() => {
        fetchItemLocations();
    }, [fetchItemLocations]);

    return { itemLocations, error, fetchItemLocations, isLoading };
};

export default useGetItemLocations;
