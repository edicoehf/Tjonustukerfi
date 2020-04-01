import React from "react";
import customerService from "../services/customerService";

const useUpdateCustomer = () => {
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
                })
                .catch(error => setError(error))
                .finally(() => {
                    setValues(null);
                    setProcessing(false);
                });
        }
    }, [isProcessing, values]);

    const handleUpdate = values => {
        if (!isProcessing) {
            setValues(values);
        }
    };

    return { updateError, handleUpdate, isProcessing };
};

export default useUpdateCustomer;
