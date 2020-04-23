import React from "react";
import orderService from "../services/orderService";

const useUpdateOrder = (initCb) => {
    const [updateError, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [values, setValues] = React.useState(null);
    const [cb, setCb] = React.useState(initCb);

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

    const handleUpdate = (values, paraCb) => {
        if (!isProcessing) {
            if (paraCb) {
                setCb(paraCb);
            }
            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing };
};

export default useUpdateOrder;
