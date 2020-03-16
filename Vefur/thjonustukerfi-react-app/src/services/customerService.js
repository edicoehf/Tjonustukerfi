import { handleErrors } from './serviceHandlers';

const endpoint = "http://localhost:5000/api/customers/";

const createCustomer = customer => {
    return fetch(endpoint, {
        method: "POST",
        body: JSON.stringify(customer),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true
        }
    });
};

const getCustomerById = id => {
    return fetch(endpoint + id, {
        method: "GET",
        headers: {
            crossDomain: true
        }
    })
    .then(handleErrors)
    .then(data => {
        if(!data) { return {}; }
        return data;
    })
    .catch(error => Promise.reject(error));
}

export default {
    createCustomer,
    getCustomerById
};
