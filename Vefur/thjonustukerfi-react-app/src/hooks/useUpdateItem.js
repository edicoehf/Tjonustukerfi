import React from "react";
import itemService from "../services/itemService";

const useUpdateItem = (cb) => {
    const [updateError, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [values, setValues] = React.useState(null);

    React.useEffect(() => {
        if (values && !isProcessing) {
            setProcessing(true);
            itemService
                .updateItemById(values)
                .then(() => {
                    setError(null);
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setValues(null);
                    setProcessing(false);
                    if (cb) {
                        cb();
                    }
                });
        }
    }, [isProcessing, values, cb]);

    const handleUpdate = (values) => {
        if (!isProcessing) {
            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing };
};

export default useUpdateItem;
