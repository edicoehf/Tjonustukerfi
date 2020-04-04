import { handleErrors, handleData } from "./serviceHandlers";

const endpoint = "http://localhost:5000/api/customers/";

const getAllCustomers = () => {
    return fetch(endpoint, {
        method: "GET",
        headers: {
            crossDomain: true
        }
    })
        .then(handleErrors)
        .then(handleData)
        .catch(error => Promise.reject(error));
};

const deleteCustomerById = id => {
    console.log(id);

    return fetch(endpoint + id, {
        method: "DELETE",
        headers: {
            crossDomain: true
        }
    })
        .then(handleErrors)
        .catch(error => Promise.reject(error));
};

const createCustomer = customer => {
    return fetch(endpoint, {
        method: "POST",
        body: JSON.stringify(customer),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true
        }
    })
        .then(handleErrors)
        .catch(error => Promise.reject(error));
};

const getCustomerById = id => {
    return fetch(endpoint + id, {
        method: "GET",
        headers: {
            crossDomain: true
        }
    })
        .then(handleErrors)
        .then(handleData)
        .catch(error => Promise.reject(error));
};

const updateCustomer = customer => {
    console.log(customer);
    return fetch(endpoint + customer.id + "/update", {
        method: "PATCH",
        body: JSON.stringify(customer),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true
        }
    })
        .then(handleErrors)
        .catch(error => Promise.reject(error));
};

export default {
    getAllCustomers,
    deleteCustomerById,
    createCustomer,
    getCustomerById,
    updateCustomer
};
