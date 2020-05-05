import React from "react";
import itemService from "../services/itemService";

const useFindItem = () => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(false);

    const fetchItemById = React.useCallback((id) => {
        setIsLoading(true);
        itemService
            .getItemById(id)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    const fetchItemByBarcode = React.useCallback((barcode) => {
        setIsLoading(true);
        itemService
            .getItemByBarcode(barcode)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    return { item, error, fetchItemById, fetchItemByBarcode, isLoading };
};

export default useFindItem;
