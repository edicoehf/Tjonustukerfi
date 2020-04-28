import React from "react";
import itemService from "../services/itemService";

const useGetItemByBarcode = (barcode) => {
    const [item, setItem] = React.useState({});
    const [error, setError] = React.useState(null);

    const fetchItem = React.useCallback(() => {
        itemService
            .getItemByBarcode(barcode)
            .then((item) => {
                setItem(item);
                setError(null);
            })
            .catch((error) => setError(error));
    }, [barcode]);

    React.useEffect(() => {
        fetchItem();
    }, [fetchItem]);

    return { item, error, fetchItem };
};

export default useGetItemByBarcode;
