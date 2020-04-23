import React from "react";
import orderService from "../services/orderService";

const useUpdateOrder = (id) => {
    const [updateError, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [values, setValues] = React.useState(null);
    const [hasUpdated, setHasUpdated] = React.useState(false);

    React.useEffect(() => {
        if (values && !isProcessing) {
            setProcessing(true);
            orderService
                .updateOrderById(values, id)
                .then(() => {
                    setError(null);
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setValues(null);
                    setProcessing(false);
                    setHasUpdated(true);
                });
        }
    }, [isProcessing, values, id]);

    const handleUpdate = (values) => {
        if (!isProcessing) {
            setHasUpdated(false);

            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing, hasUpdated };
};

export default useUpdateOrder;
