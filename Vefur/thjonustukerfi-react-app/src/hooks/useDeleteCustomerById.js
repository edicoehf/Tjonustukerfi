import React from "react";
import customerService from "../services/customerService";

const useDeleteCustomerById = id => {
    const [error, setError] = React.useState(null);
    const [isDeleting, setDeleting] = React.useState(false);

    React.useEffect(() => {
        if (isDeleting) {
            customerService
                .deleteCustomerById(id)
                .then(() => {
                    setError(null);
                })
                .catch(error => setError(error))
                .finally(() => setDeleting(false));
        }
    }, [id]);

    const handleDelete = () => {
        if (!isDeleting) {
            setDeleting(true);
        }
    };

    return { error, handleDelete, isDeleting };
};

export default useDeleteCustomerById;
