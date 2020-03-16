import React from 'react';
import customerService from '../../services/customerService';

const emptyCustomer = {
    id: "",
    name: "",
    ssn: "",
    telephone: "",
    email: "",
    postalCode: "",
    address: ""
};

const useCustomerService = id => {
    const [customer, setCustomer] = React.useState(emptyCustomer);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        customerService.getCustomerById(id)
            .then(customer => {
                setCustomer(customer);
                setError(null);
            })
            .catch(error => setError(error))
    }, []);

    return { customer, error }
};

export default useCustomerService;