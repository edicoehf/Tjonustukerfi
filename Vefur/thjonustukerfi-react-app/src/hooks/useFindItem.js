import React from "react";
import itemService from "../services/itemService";

const useFindItem = () => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);

    const fetchItemById = React.useCallback((id) => {
        itemService
            .getItemById(id)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error));
    }, []);

    const fetchItemByBarcode = React.useCallback((barcode) => {
        itemService
            .getItemByBarcode(barcode)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error));
    }, []);

    return { item, error, fetchItemById, fetchItemByBarcode };
};

export default useFindItem;
