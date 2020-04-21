import React from "react";
import itemService from "../services/itemService";

const useUpdateItem = () => {
    const [updateError, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [values, setValues] = React.useState(null);

    React.useEffect(() => {
        if (values && !isProcessing) {
            setProcessing(true);
            itemService
                .updateItem(values)
                .then(() => {
                    setError(null);
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setValues(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, values]);

    const handleUpdate = (values) => {
        if (!isProcessing) {
            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing };
};

export default useUpdateItem;
