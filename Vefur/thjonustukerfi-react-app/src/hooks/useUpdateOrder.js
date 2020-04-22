import React from "react";
import orderService from "../services/orderService";

const useUpdateOrder = (cb) => {
    const [updateError, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [values, setValues] = React.useState(null);

    React.useEffect(() => {
        if (values && !isProcessing) {
            setProcessing(true);
            orderService
                .updateOrderById(values)
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

export default useUpdateOrder;
