import React from "react";
import customerService from "../services/customerService";

const useUpdateCustomer = (cb) => {
    const [updateError, setError] = React.useState(null);
    const [isProcessing, setProcessing] = React.useState(false);
    const [values, setValues] = React.useState(null);

    React.useEffect(() => {
        if (values && !isProcessing) {
            setProcessing(true);
            customerService
                .updateCustomer(values)
                .then(() => {
                    setError(null);
                    if (cb) {
                        cb();
                    }
                })
                .catch((error) => setError(error))
                .finally(() => {
                    setValues(null);
                    setProcessing(false);
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

export default useUpdateCustomer;
